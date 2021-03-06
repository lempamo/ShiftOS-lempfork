/*
 * MIT License
 * 
 * Copyright (c) 2017 Michael VanOverbeek and ShiftOS devs
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiftOS.Objects;
using ShiftOS.Engine;


namespace ShiftOS.WinForms
{
    public class AcquireCodepointsJobTask : JobTask
    {
        public AcquireCodepointsJobTask(int amount)
        {
            CodepointsRequired = SaveSystem.CurrentSave.Codepoints + amount;
        }

        public long CodepointsRequired { get; private set; }

        public override bool IsComplete
        {
            get
            {
                return (SaveSystem.CurrentSave.Codepoints >= CodepointsRequired);
            }
        }
    }

    public class AcquireUpgradeJobTask : JobTask
    {
        public AcquireUpgradeJobTask(string upgId)
        {
            UpgradeID = upgId;
        }

        public string UpgradeID { get; private set; }

        public override bool IsComplete
        {
            get
            {
                return Shiftorium.UpgradeInstalled(UpgradeID);
            }
        }
    }
}
