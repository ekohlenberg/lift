using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
    public class Phrase : BaseLiftDomain
    {
        public IntProperty id = new IntProperty();
        public StringProperty label = new StringProperty();
        public IntProperty language_id = new IntProperty();
        public StringProperty phrase = new StringProperty();

        public Phrase()
        {
            BaseTable = "phrases";
            AutoIdentity = false;
            PrimaryKey = "id";

            attach("id", id);
            attach("label", label);
            attach("language_id", language_id);
            attach("phrase", phrase);
        }
    }
}
