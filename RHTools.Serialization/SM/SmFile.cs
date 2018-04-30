using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.SM
{
    // Full list of tags: https://github.com/stepmania/stepmania/blob/4df85929cc2e41ef8c24bddea17d90fe44fb2807/src/NotesLoaderSM.cpp#L217
    public class SmFile : ITextSerializable
	{
        public string title;
        public string subtitle;
        public string artist;
        public string titleTranslit; // Translation
        public string subtitleTranslit;
        public string artistTranslit;
        public string genre;
        public string credit;
        public string banner;
        public string background;
        public string lyricsPath;
        public string cdTitle;
        public string music;
        public float offset;
        public Bpms bpms;
        public Stops stops;
        public float sampleStart;
        public float sampleLength;
        public DisplayBpm displayBpm;
        public bool selectable;
        public BackgroundChanges bgChanges;
        public BackgroundChanges fgChanges;
        public List<Chart> charts;

        public SmFile()
        {
            charts = new List<Chart>();
        }

        public void Serialize(StreamWriter writer)
        {
            throw new NotImplementedException();
        }

        public static SmFile Deserialize(StreamReader reader)
        {
            SmFile file = new SmFile();

            MsdFile msdFile = MsdFile.Deserialize(reader);
            foreach (MsdValue value in msdFile.values)
            {
                List<string> parameters = value.parameters;
                string tag = parameters[0].ToUpper();
                switch (tag)
                {
                    case "TITLE":
                        file.title = parameters[1];
                        break;
                    case "SUBTITLE":
                        file.subtitle = parameters[1];
                        break;
                    case "ARTIST":
                        file.artist = parameters[1];
                        break;
                    case "TITLETRANSLIT":
                        file.titleTranslit = parameters[1];
                        break;
                    case "SUBTITLETRANSLIT":
                        file.subtitleTranslit = parameters[1];
                        break;
                    case "ARTISTTRANSLIT":
                        file.artistTranslit = parameters[1];
                        break;
                    case "GENRE":
                        file.genre = parameters[1];
                        break;
                    case "CREDIT":
                        file.credit = parameters[1];
                        break;
                    case "BANNER":
                        file.background = parameters[1];
                        break;
                    case "BACKGROUND":
                        file.background = parameters[1];
                        break;
                    case "LYRICSPATH":
                        file.lyricsPath = parameters[1];
                        break;
                    case "CDTITLE":
                        file.cdTitle = parameters[1];
                        break;
                    case "MUSIC":
                        file.music = parameters[1];
                        break;
                    case "OFFSET":
						float.TryParse(parameters[1], out file.offset);
                        break;
                    case "BPMS":
                        file.bpms = Bpms.Deserialize(parameters);
                        break;
                    case "STOPS":
                        file.stops = Stops.Deserialize(parameters);
                        break;
                    case "SAMPLESTART":
						float.TryParse(parameters[1], out file.sampleStart);
                        break;
                    case "SAMPLELENGTH":
						float.TryParse(parameters[1], out file.sampleLength);
                        break;
                    case "DISPLAYBPM":
                        file.displayBpm = DisplayBpm.Deserialize(parameters);
                        break;
                    case "SELECTABLE":
                        file.selectable = IsSelectable(parameters);
                        break;
                    case "BGCHANGES":
                        file.bgChanges = BackgroundChanges.Deserialize(parameters);
                        break;
                    case "FGCHANGES":
                        file.fgChanges = BackgroundChanges.Deserialize(parameters);
                        break;
                    case "NOTES":
                        file.charts.Add(Chart.Deserialize(parameters));
                        break;
                    default:
                        Console.Out.WriteLine("Invalid tag: " + tag);
                        break;
                }
            }

            return file;
        }

        private static bool IsSelectable(List<string> parameters)
        {
            return parameters[1] == SelectableType.YES.ToString();
        }
    }
}
