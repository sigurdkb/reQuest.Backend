using System;
using System.Collections.Generic;

namespace reQuest.Backend.ViewModels
{
    public class DataportenTokenDto
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
        public int Expires_In { get; set; }
        public string Scope { get; set; }
        public string Id_Token { get; set; }

    }
}

// {
//     "access_token": "74b4b8d8-4122-4160-bc12-cddfe5538c54",
//     "token_type": "Bearer",
//     "expires_in": 18734,
//     "scope": "email groups openid profile userid userid-feide",
//     "id_token": "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpc3MiOiJodHRwczpcL1wvYXV0aC5kYXRhcG9ydGVuLm5vIiwiYXVkIjoiN2E3MWU3ZmUtMTFkNS00NTJhLWE2NmItYjdjZmE4Yjg0OWFiIiwic3ViIjoiODRmNjkwNDktN2FlOC00NmM1LWI3MjQtMWJjZjBjZDYxMWRkIiwiaWF0IjoxNDgzMjcyMTA4LCJleHAiOjE0ODMyNzU3MDgsImF1dGhfdGltZSI6MTQ4MzI2MjA0MX0.ozirm3yKJaP_iYnoaHrQDe4MjNniow3V2aKQmQkl1vHCIfgOhOUdfkswBK3aBadYZP93rs-JDKKMCfTNe83swQNiEVSdtUZEd547q0OVwYn5Zc4caheu5cmAbLJ9O-xr1RWY0JiFhp2x99X2HNF2MrDtaYtBxd-7O8YbhyDlGlPtmqFd3uFnsSTkeNTi2ddIH8hXDfV-SNCGIlScLPewGcCPzIR9DBtMhNAYMdIPWIuPZtKvfXJRLhQhxJSqM37JI4ue1n5NN_Zr8R14kBfM27BSzFogxrjOVj_ZtI_Y-Z_d2PAYCEeIqtNcq6SD3yBmMms3AxWVPoZdgz66wkj6dg"
// }