using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Models
{


    public class person
    {
        public string name { get; set; }
        public int phoneNumbr { get; set; }
    }


    public interface irepo<T>
    {
        void addAddperson(T item);
        string DidpersonAdd();
    }





    public class generic<T> : irepo<person>
    {

        string[] mydabase = new string[2];
        public void addAddperson(person person)
        {


            mydabase[0] = person.name;
            mydabase[1] = person.phoneNumbr.ToString();

        }
        public string DidpersonAdd()
        {
            return mydabase[0] + mydabase[1] + " hisweswo swi swa wena";
        }


    }
}