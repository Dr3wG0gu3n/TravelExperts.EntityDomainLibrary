/*This Validator class was created by Andrew Goguen on Jan. 15, 2014
 * for the Travel Experts Threaded Project
 * It validates the Controls and The Input from the User
 * It is a static class with multiple boolean methods for validating
 * text fields, integer fields, etc.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; //add a forms reference So we can access the Control class

namespace TravelExperts.EntityDomainLibrary
{
    public static class Validator
    {
        /// <summary>
        /// A Method to make sure a given control is not empty
        /// </summary>
        /// <param name="control">Control</param>
        /// <returns>bool</returns>
        public static bool IsPresent(Control control)  
        {
            if (control.GetType().ToString() == "System.Windows.Forms.TextBox")
            {
                TextBox textBox = (TextBox)control;
                if (textBox.Text != "")
                {
                    return true;
                }
            }
            else if (control.GetType().ToString() == "System.Windows.Forms.ComboBox")
            {
                ComboBox comboBox = (ComboBox)control;
                if (comboBox.SelectedIndex == -1)
                {
                    return true;
                }
            }
            else if (control.GetType().ToString() == "System.Windows.Forms.Label")
            {
                Label label = (Label)control;
                if (label.Text != "")
                {
                    return true;
                }
            }
            
            return false;
        }

        /// <summary>
        /// checks the textbox for a numeric value
        /// </summary>
        /// <param name="textbox">textbox</param>
        /// <returns>Boolean</returns>
        public static bool IsNumeric(TextBox textbox)
        {
            try { int.Parse(textbox.Text); return true; }
            catch { }
            try { long.Parse(textbox.Text); return true; }
            catch { }
            try { ulong.Parse(textbox.Text); return true; }
            catch { }
            try { float.Parse(textbox.Text); return true; }
            catch { }
            try { double.Parse(textbox.Text); return true; }
            catch { }
            try { decimal.Parse(textbox.Text); return true; }
            catch { }
            return false;
        }

        /// <summary>
        /// check each character to make sure it is Alphabetical returning true if it passes
        /// </summary>
        /// <param name="textbox"></param>
        /// <returns></returns>
        public static bool IsAlphabetical(TextBox textbox)
        {
            string words = textbox.Text;
            string[] word;
            char[] characters;

            word = words.Split();

            foreach (string w in word)  // in case multiple words with spaces are passed in
            {
                characters = w.ToCharArray();
                foreach (char c in characters)
                {
                    if (!Char.IsLetter(c))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// A method to format a name passed in 
        /// </summary>
        /// <param name="name">string</param>
        /// <returns>the String</returns>
        public static string FormatedName(string name)
        {
            //declare the variables that we use locally
            string formattedName = "";
            string[] names;
            char[] letters;

            name = name.Trim();
            names = name.Split();

            foreach (string n in names)
            {
                if (n == "") //this handles the multiple spaces between names
                {
                    continue;
                }
                else
                {
                    letters = n.ToCharArray();                                  //once validation has passed break into array of Chars
                    formattedName += letters[0].ToString().ToUpper();       //Capitalize the first letter

                    for (int i = 1; i < letters.Length; i++)
                    {
                        formattedName += letters[i].ToString().ToLower();   //make sure all other letters 
                    }                                                               //are lowercase and add them to the name
                    formattedName += " ";                                   //Add a space between names
                }
            }                
            return formattedName.TrimEnd();
        }
    }
}
