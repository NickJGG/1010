using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1010.Buttons {
    public class Button : GameObject {
        public Button(string text) {
            Text = text;
        }

        public string Text { get; set; }
    }
}
