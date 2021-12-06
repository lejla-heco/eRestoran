using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.Helper
{
    public class Student4VM
    {
        public string ImePrezime { get; set; }
        public string RadnoMjesto { get; set; }
        public string Opis { get; set; }
        public string SlikaPutanja { get; set; }
        public string ID { get; set; }
    }
    public class Data
    {
        public static List<Student4VM> listRadnici => new List<Student4VM>
            {
                new Student4VM
                {   ID = "1",
                    ImePrezime = "Radnik 00001",
                    RadnoMjesto = "Programiranje",
                    Opis = "Phasellus 1 eget enim eu lectus faucibus vestibulum. Suspendisse sodales pellentesque elementum.",
                    SlikaPutanja = "https://restapiexample.wrd.app.fit.ba/profile_images/team2.png"
                }  ,
                new Student4VM
                {   ID = "2",
                    ImePrezime = "Radnik 00002",
                    RadnoMjesto = "Programiranje",
                    Opis = "Phasellus 2 eget enim eu lectus faucibus vestibulum. Suspendisse sodales pellentesque elementum.",
                    SlikaPutanja = "https://restapiexample.wrd.app.fit.ba/profile_images/team3.png"
                },
                new Student4VM
                {   ID = "3",
                    ImePrezime = "Radnik 00003",
                    RadnoMjesto = "Programiranje",
                    Opis = "Phasellus 3 eget enim eu lectus faucibus vestibulum. Suspendisse sodales pellentesque elementum.",
                    SlikaPutanja = "https://restapiexample.wrd.app.fit.ba/profile_images/team4.png"
                },
                new Student4VM
                {   ID = "4",
                    ImePrezime = "Radnik 00004",
                    RadnoMjesto = "Programiranje",
                    Opis = "Phasellus 4 eget enim eu lectus faucibus vestibulum. Suspendisse sodales pellentesque elementum.",
                    SlikaPutanja = "https://restapiexample.wrd.app.fit.ba/profile_images/team1.png"
                }
                ,
                new Student4VM
                {   ID = "5",
                    ImePrezime = "Radnik 00005",
                    RadnoMjesto = "Web development",
                    Opis = "Phasellus 4 eget enim eu lectus faucibus vestibulum. Suspendisse sodales pellentesque elementum.",
                    SlikaPutanja = "https://restapiexample.wrd.app.fit.ba/profile_images/team5.png"
                },
                new Student4VM
                {   ID = "6",
                    ImePrezime = "Radnik 00006",
                    RadnoMjesto = "Web development",
                    Opis = "Phasellus 4 eget enim eu lectus faucibus vestibulum. Suspendisse sodales pellentesque elementum.",
                    SlikaPutanja = "https://restapiexample.wrd.app.fit.ba/profile_images/team6.png"
                },
                new Student4VM
                {   ID = "7",
                    ImePrezime = "Radnik 00007",
                    RadnoMjesto = "Web development",
                    Opis = "Phasellus 4 eget enim eu lectus faucibus vestibulum. Suspendisse sodales pellentesque elementum.",
                    SlikaPutanja = "https://restapiexample.wrd.app.fit.ba/profile_images/team7.png"
                },
                new Student4VM
                {   ID = "8",
                    ImePrezime = "Radnik 00008",
                    RadnoMjesto = "Web development",
                    Opis = "Phasellus 4 eget enim eu lectus faucibus vestibulum. Suspendisse sodales pellentesque elementum.",
                    SlikaPutanja = "https://restapiexample.wrd.app.fit.ba/profile_images/team8.png"
                }
                ,
                new Student4VM
                {   ID = "9",
                    ImePrezime = "Radnik 00009",
                    RadnoMjesto = "Web design",
                    Opis = "Phasellus 4 eget enim eu lectus faucibus vestibulum. Suspendisse sodales pellentesque elementum.",
                    SlikaPutanja = "https://restapiexample.wrd.app.fit.ba/profile_images/team9.png"
                },
                new Student4VM
                {   ID = "10",
                    ImePrezime = "Radnik 00010",
                    RadnoMjesto = "Web design",
                    Opis = "Phasellus 4 eget enim eu lectus faucibus vestibulum. Suspendisse sodales pellentesque elementum.",
                    SlikaPutanja = "https://restapiexample.wrd.app.fit.ba/profile_images/team10.png"
                },
                new Student4VM
                {   ID = "11",
                    ImePrezime = "Radnik 00011",
                    RadnoMjesto = "Web design",
                    Opis = "Phasellus 4 eget enim eu lectus faucibus vestibulum. Suspendisse sodales pellentesque elementum.",
                    SlikaPutanja = "https://restapiexample.wrd.app.fit.ba/profile_images/team11.png"
                },
                new Student4VM
                {   ID = "12",
                    ImePrezime = "Radnik 00012",
                    RadnoMjesto = "Web design",
                    Opis = "Phasellus 4 eget enim eu lectus faucibus vestibulum. Suspendisse sodales pellentesque elementum.",
                    SlikaPutanja = "https://restapiexample.wrd.app.fit.ba/profile_images/team12.png"
                }
            };
    }
}
