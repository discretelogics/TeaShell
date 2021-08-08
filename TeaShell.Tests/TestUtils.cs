using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TeaTime;

namespace TeaShell.Tests
{
    class TestUtils
    {
        /// <summary>
        /// Creates a TeaFile with Time/Integer items and 7 values.
        /// </summary>
        /// <returns></returns>
        public static string CreateTestTeaFile()
        {
            const string filename = "test.tea";
            using (var tf = TeaFile<Event<int>>.Create(filename))
            {
                var t = new Time(2000, 1, 1);
                for (int i = 0; i < 7; i++)
                {
                    tf.Write(new Event<int> { Time = t, Value = i * 1100 });
                    t = t.AddDays(1);
                }
            }
            return filename;
        }
    }
}
