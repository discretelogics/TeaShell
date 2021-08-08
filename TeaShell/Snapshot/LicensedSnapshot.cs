using System;

namespace TeaTime.TeaShell
{
    class LicensedSnapshot
    {
        public TeaFileSnapshot Snapshot { get; set; }
		public string FileName { get; set; }
		public string L { get; set; }	// Licensee
        public bool V { get; set; }		// IsValid
        public bool E { get; set; }		// IsExpired
        public bool EV { get; set; }	// IsEvaluation
    }
}
