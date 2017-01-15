using System;
using System.Collections.Generic;

namespace reQuest.Backend.ViewModels
{
    public class DataportenGroupDto
    {
        public string Parent { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public DataportenGroupMembership Membership { get; set; } = new DataportenGroupMembership();

    }
    public class DataportenGroupMembership
    {
        public bool Active { get; set; }
        public string Basic { get; set; }
        public string DisplayName { get; set; }
        public string SubjectRelations { get; set; }
        public List<string> Fsroles { get; set; } = new List<string>();
        public DateTime NotAfter { get; set; } = new DateTime();

    }
}

// [
//     {
//         "parent": "fc:org:uia.no",
//         "membership": {
//             "active": true,
//             "fsroles": [
//                 "STUDENT"
//             ],
//             "displayName": "Student",
//             "subjectRelations": "undervisning",
//             "notAfter": "2016-08-14T22:00:00Z",
//             "basic": "member"
//         },
//         "id": "fc:fs:fs:emne:uia.no:DAT219:G",
//         "url": "http://www.uia.no/studieplaner/topic/DAT219-G",
//         "displayName": "Internettjenester",
//         "type": "fc:fs:emne"
//     },
// ]
