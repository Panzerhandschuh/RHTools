﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.SM
{
    public class NoteData : ITextSerializable
	{
        public List<Measure> measures;

        public NoteData()
        {
            measures = new List<Measure>();
        }

        public void Serialize(StreamWriter writer)
        {
            throw new NotImplementedException();
        }

        public static NoteData Deserialize(string parameter)
        {
            var data = new NoteData();

            var measureValues = parameter.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var measureValue in measureValues)
                data.measures.Add(Measure.Deserialize(measureValue));

            return data;
        }
    }
}
