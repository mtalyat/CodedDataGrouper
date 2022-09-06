using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedDataGrouper
{
    /// <summary>
    /// A list of events. These are the groups that are created from the excel data.
    /// </summary>
    internal class EventList
    {
        public class Event
        {
            public int GroupID { get; private set; }

            public double Time { get; private set; }

            public List<RowData> RowDatas { get; private set; }

            public Event()
            {
                GroupID = -1;
                Time = 0.0;
                RowDatas = new List<RowData>();
            }

            public Event(int behaviorId, double time, RowData initialRow)
            {
                GroupID = behaviorId;
                Time = time;
                RowDatas = new List<RowData>();
                RowDatas.Add(initialRow);
            }

            public override string ToString()
            {
                return $"[{RowDatas.Count} {RowDatas[0].Group} events at {Time}]";
            }
        }

        public int RowCount { get; private set; } = 0;

        public int ObvserverCount => _observerIDs.Count;
        private Dictionary<int, string> _observerIDs = new Dictionary<int, string>();

        private Dictionary<int, SortedDictionary<int, Event>> _events = new Dictionary<int, SortedDictionary<int, Event>>();

        public EventList()
        {

        }

        private SortedDictionary<int, Event> GetList(int categoryID)
        {
            SortedDictionary<int, Event> list;

            if (_events.TryGetValue(categoryID, out list))
            {
                //found existing list
                return list;
            }

            //new list, one does not exist yet
            list = new SortedDictionary<int, Event>();
            _events.Add(categoryID, list);

            return list;
        }

        private bool CanGroupTogether(RowData rd, Event ev)
        {
            //make sure there is not a row with an existing observation ID that matches, and group ID
            bool idMatch = false;
            bool groupMatch = false;

            //can only match with valid IDs
            if (rd.IDID >= 0)
            {
                foreach (RowData r in ev.RowDatas)
                {
                    if (r.IDID == rd.IDID)
                    {
                        idMatch = true;
                    }
                    if (r.GroupID == rd.GroupID)
                    {
                        groupMatch = true;
                    }

                    if (idMatch && groupMatch)
                    {
                        break;
                    }
                }
            }

            return !idMatch && groupMatch;
        }

        public void Add(RowData rd, ConfigurationData data)
        {
            //add to counts
            _observerIDs.TryAdd(rd.IDID, rd.ID);
            RowCount++;

            //figure out the threshold based on the behavior type
            double threshold = rd.Category.Threshold;
            SortedDictionary<int, Event> events = GetList(rd.CategoryID);

            //if the row has a valid group ID, then use that instead of finding a group
            if(rd.GroupIDID >= 0)
            {
                //get existing group that this line belongs to
                if(events.TryGetValue(rd.GroupIDID, out Event? evnt))
                {
                    //group exists, add to it
                    evnt.RowDatas.Add(rd);
                } else
                {
                    //no event, create one
                    evnt = new Event(rd.GroupID, rd.AverageTime, rd);
                    events.Add(rd.GroupIDID, evnt);
                }

                return;
            }

            Event ev;

            //check for existing events
            for (int i = events.Count - 1; i >= 0; i--)
            {
                //if within the threshold, and the row data matches, add to the event
                ev = events[i];

                //check if the new row data is within range of the event
                if(rd.AverageTime <= ev.Time + threshold)
                {
                    if(CanGroupTogether(rd, ev))
                    {
                        //no other observation ID with the same ID

                        //add to existing event
                        ev.RowDatas.Add(rd);

                        //done adding
                        return;
                    }

                    //keep searching for one that is in the same time, and has the same behavior id
                } else
                {
                    //not in range, stop searching
                    break;
                }
            }

            //no longer searching
            //add to most recent event
            //if unable, add to new event
            int[] superGroupIDs = data.GetSuperGroupIDs(rd.GroupID);

            if (superGroupIDs.Any())
            {
                for (int i = events.Count - 1; i >= 0; i--)
                {
                    ev = events[i];

                    if (rd.AverageTime <= ev.Time + threshold)
                    {
                        //make sure there is not a row with an existing observation ID that matches
                        bool idMatch = false;
                        bool groupMatch = false;

                        //can only match with valid IDs
                        if (rd.IDID >= 0)
                        {
                            foreach (RowData r in ev.RowDatas)
                            {
                                if (r.IDID == rd.IDID)
                                {
                                    idMatch = true;
                                }
                                if (data.CheckMatchingSuperGroups(r.GroupID, superGroupIDs))
                                {
                                    groupMatch = true;
                                }
                                if (idMatch && groupMatch)
                                {
                                    break;
                                }
                            }
                        }

                        if (!idMatch && groupMatch)
                        {
                            //able to add together: in the same super group
                            ev.RowDatas.Add(rd);
                            return;
                        }
                    } else
                    {
                        //cannot fit in any events
                        break;
                    }
                }                
            }

            //must add to it's own event

            //get key for it
            int groupIDID = events.Keys.Any() ? events.Keys.Last() + 1 : 0;
            events.Add(groupIDID, new Event(rd.GroupID, rd.AverageTime, rd));
        }

        /// <summary>
        /// Returns a combined list with the State and Point events, in that order.
        /// </summary>
        /// <returns></returns>
        public List<Event> GetEvents()
        {
            if(!_events.Any())
            {
                return new List<Event>();
            }

            List<List<Event>> events = _events.Values.Select(sd => sd.Values.ToList()).ToList();

            //sort by categories, if categories are valid
            if(events[0][0].RowDatas[0].CategoryID >= 0)
            {
                events.Sort((x, y) => x[0].RowDatas[0].Category.Name.CompareTo(y[0].RowDatas[0].Category.Name));
            }

            //combine into one list
            List<Event> combined = new List<Event>();

            foreach (var list in events)
            {
                combined.AddRange(list);
            }

            return combined;
        }

        public int GetEventCount()
        {
            int count = 0;

            foreach(var pair in _events)
            {
                count += pair.Value.Count;
            }

            return count;
        }

        public List<RowData> GetRowDatas()
        {
            List<RowData> rds = new List<RowData>();
            foreach(Event e in GetEvents())
            {
                rds.AddRange(e.RowDatas);
            }

            return rds;
        }

        public List<string> GetObserverIDs()
        {
            return _observerIDs.Values.ToList();
        }
    }
}
