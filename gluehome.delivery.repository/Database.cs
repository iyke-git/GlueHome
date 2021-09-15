using System;
using System.Collections.Generic;
using gluehome.delivery.repository.models;

namespace gluehome.delivery.repository
{
    internal static class Database
    {
        public static List<Partner> Partners = new List<Partner>{
            new Partner{
                name="abc ltd",
                areas=new List<string>{"nottingham","leicester"},
                id=new Guid("65d0b644-7514-42a2-9471-a2c6edb0cca6")
            },
            new Partner{
                name="xyz ltd",
                areas=new List<string>{"nottingham","manchester"},
                id=new Guid("0d6388aa-7b8d-4590-accc-e9f6812866c6")
            },
            new Partner{
                name="ben logistics",
                areas=new List<string>{"manchester","liverpool"},
                id=new Guid("c0182dd4-80b9-45e9-ad76-e9ecddf09181")
            },
            new Partner{
                name="matt ent",
                areas=new List<string>{"newcastle","liverpool"},
                id=new Guid("21396cf0-0ba9-4107-b562-8b26654f6128")
            }
        };

        public static List<Recipient> Recipients = new List<Recipient>{
            new Recipient{
                name="John Doe",
                address="Tannin Road, Nottingham",
                email="john.doe@free.com",
                phoneNumber="+447743742876"
            },
            new Recipient{
                name="James Mccall",
                address="Arnold Road, Liverpool",
                email="james.mccall@jones.com",
                phoneNumber="+4474545436777"
            },
            new Recipient{
                name="Terry Wills",
                address="Wooding Road, Manchester",
                email="terry.wills@ruminate.com",
                phoneNumber="+4477453578854"
            },
            new Recipient{
                name="Micheal Abbas",
                address="Company Road, Newcastle",
                email="micheal.abbas@oldnavy.com",
                phoneNumber="+447745342876"
            }
        };

        public static List<Order> Orders = new List<Order>
        {
            new Order{
                orderNumber="12439667",
                sender="Ikea"
            },
            new Order{
                orderNumber="12337797",
                sender="Sainsbury"
            },
            new Order{
                orderNumber="35467821",
                sender="Wayfair"
            }
        };
        public static List<Delivery> Deliveries = new List<Delivery>();
        public static List<AccessWindow> AccessWindows = new List<AccessWindow>();

    }
}
