using SafeNeeds.DySmat.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeNeeds.DySmat.Logic
{
    public class DyNoteLogic : DyLogicBase
    {
        public string SendNode(Y_Note note) {

            note.NoteCD = Guid.NewGuid().ToString().Replace("-", "");

            note.Status = "new";

            note.SendTime = DateTime.Now;

            note.UpdateTime = DateTime.Now;

            db.Notes.Add(note);

            db.SaveChanges();

            return note.NoteCD;
        }

        public List<Y_Note> GetNodes(int ProjID, string NoteUserCD, DateTime? SendTimeFrom)
        {
            List<Y_Note> result = new List<Y_Note>();



            if (SendTimeFrom == null)
            {
                result = db.Notes.Where(n => n.ProjID == ProjID 
                    && n.NoteUserCD == NoteUserCD
                    && n.SendTime <= DateTime.Now
                    && n.Status != "readed").ToList();
            }
            else {
                result = db.Notes.Where(n => n.ProjID == ProjID
                       && n.NoteUserCD == NoteUserCD
                       && n.SendTime >= SendTimeFrom
                       && n.SendTime <= DateTime.Now
                       && n.Status != "readed").ToList();
            }

            return result;
        }

        public bool SaveNodeStatus(string NoteCD, string Status)
        {
            Y_Note note = db.Notes.Find(NoteCD);

            if (note != null) {

                note.Status = Status;
                note.UpdateTime = DateTime.Now;

                db.Notes.AddOrUpdate(note);

                db.SaveChanges();
            }

            return true;
        }
    }
}
