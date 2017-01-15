using System;
using System.Collections.Generic;

namespace reQuest.Backend.ViewModels
{
    public class DataportenUserDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ProfilePhoto { get; set; }
        public List<string> UserId_Sec { get; set; } = new List<string>();

    }
}

// "user": {
//     "userid_sec": [
//         "feide:sigurdkb@uia.no"
//     ],
//     "userid": "84f69049-7ae8-46c5-b724-1bcf0cd611dd",
//     "name": "Sigurd Kristian Brinch",
//     "email": "sigurd.k.brinch@uia.no",
//     "profilephoto": "p:d41622a8-1a85-439b-9833-21a7052f3cb7"
// },

