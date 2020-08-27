using System;

namespace ALTechTest.Classes.MusicBrainz
{
    public class Relationship
    {
        public AttributeIds attributeids { get; set; }
        public string[] attributes { get; set; }
        public object attributevalues { get; set; }
        public string begin { get; set; }
        public string direction { get; set; }
        public string end { get; set; }
        public bool ended { get; set; }
        public int orderingkey { get; set; }
        public Recording recording { get; set; }
        public string sourcecredit { get; set; }
        public string targetcredit { get; set; }
        public string targettype { get; set; }
        public string type { get; set; }
        public Guid typeid { get; set; }
        public Work work { get; set; }
    }
}