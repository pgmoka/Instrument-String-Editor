// Copyright (c) Pedro Gonvalves Mokarzel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intrument_Editor
{
    class editor
    {
        private String[] instrumentTab;
        private int instrumentStringNumber;
        private String[] tune;
        protected static String[] instruments = {"Guitar"};

        private static int added = 0;

        public int LIMIT_OF_CHARS = 105;
        

        /**
        * Creates a Constructor that sets the number of String in the instrument, and the tuning
        * precondition: a int, and a String[]
        * @param instrument number of Strings as an int
        * @param tuning tune as a String[] where it goes from right to left
        */
        public editor(int instrument, String[] tuning)
        {
            this.instrumentStringNumber = instrument;
            this.instrumentTab = new String[0];
            this.tune = tuning;
            this.addANewTabLine(0);
        }

        /**
        * Create a fancy String out of instrument tab
        * post: String
        * @return the tab as a fancy String
        */
        public String toFancyString()
        {
            String str = "";
            //Adds instrument String as a String
            for (int i = 0; i < instrumentTab.Length; i++)
            {
                //Creates gap
                if (i % instrumentStringNumber == 0)
                {
                    if (i != 0)
                    {
                        str += "\n\n";
                    }
                }
                else
                {
                    str += "\n";
                }
                str += instrumentTab[i];
            }
            return str;
        }

        /**
         * Turns String given by the user to a String[] relevant to this instrument
         * precondition: String given by the user
         * postcondition: String[] in the same format of this instrument
         */
        public string[] fromThisFancyStringToTab(String userIn)
        {
            char[] dividing = new char[]{ '\n' };
            String[] divided = userIn.Split(dividing);
            List<String> organized = new List<String>();
            for(int i = 0; i<divided.Length;i++)
            {
                if(!(divided[i].Length<=0))
                {
                    organized.Add(divided[i]);
                }
            }
            return organized.ToArray();
        }

        /**
        * Assigns new tab to the Instrument, if it is valid
        * pre: send a String[] that is 'useful'
        * @param newTab new tab to be assigned
        */
        public void setNewInstrumentTab(String[] newTab)
        {
            //Checks if tab is valid
            if (this.isTabValidForInstrument(newTab))
            {
                this.instrumentTab = newTab;
            }
            else
            {
                Console.OpenStandardError();
            }
        }

        /**
         * Helper method that sees if a String[] is valid for the instrumentStringNumber
         * precondition: String[]
        * postcondition: boolean
        * @param tab String[] to be tested
        * @return true if it is valid, false if it is not
        */
        private bool isTabValidForInstrument(String[] tab)
        {
            if (tab.Length % this.instrumentStringNumber == 0)
            {
                return true;
            }
            return false;
        }

        //Getters
        public String[] getInstrumentTab()
        {
            return this.instrumentTab;
        }

        public int getInstrumentStringNumber()
        {
            return this.instrumentStringNumber;
        }

        //Times of tab lines:
        public int getNumberOfTabLines()
        {
            return (this.instrumentTab.Length / this.instrumentStringNumber);
        }


        /**
         * Gets size of the all lines in all the lines
         * @return a int
         */
        public int getSize()
        {
            return this.instrumentTab.Length;
        }

        /**
        * Adds a tab at a user selected position
        * precondition: sent a position to be create
        * @param position int between 0, and the last occurrence of a tab
        */
        public void addANewTabLine(int position)
        {
            added++;
            String[] newString = new String[this.getSize() + this.getInstrumentStringNumber()];
            int shift = 0;
            if (position <= this.getSize() / this.instrumentStringNumber)
            {
                //Adds old tab to new tab
                for (int i = 0; i < this.getSize(); i++)
                {
                    if (i % this.instrumentStringNumber == 0 && position == (i / (this.instrumentStringNumber - 1)) && i < (this.instrumentStringNumber*2))
                    {
                        shift += this.instrumentStringNumber;// Bug here
                    }
                    newString[i + shift] = this.getInstrumentTab()[i];
                }
                //Adds new tab, checking for exceptions:
                if (position != 0 && position <= (this.getSize() / this.instrumentStringNumber))
                {
                    //for (int i = 0; i < this.getSize(); i++)
                    //{
                        for (int j = 0; j < this.instrumentStringNumber; j++)
                        {
                            newString[j + (position * this.instrumentStringNumber)] = this.tune[j] + "|";
                        }

                    //}
                }
                else
                {
                    for (int j = 0; j < this.instrumentStringNumber; j++)
                    {
                        newString[j] = this.tune[j] + "|";
                    }
                }

                this.setNewInstrumentTab(newString);
                //Makes it all even
                this.fillWithNothing();
            }
        }

        /**
         * Removes a tab line given a position
         * Precondition: position to be removed as a int
         */
        public void removeLine(int position)
        {
            if (this.instrumentTab.Length <= this.instrumentStringNumber)
            {
                this.instrumentTab = new String[0];
            }
            else
            {
                if (position <= 0)
                {
                    position = 1;
                }else if(position > this.getNumberOfTabLines())
                {
                    position = this.getNumberOfTabLines();
                }

                String[] newString = new String[this.getSize() - this.getInstrumentStringNumber()];
                int shift = 0;
                int original = 0;
                for (int i = 0; i < this.getNumberOfTabLines(); i++)
                {
                    if ((position - 1) != i)
                    {
                        for (int j = 0; j < this.getInstrumentStringNumber(); j++)
                        {
                            newString[shift] = this.instrumentTab[original];
                            original++;
                            shift++;
                        }
                    }
                    else
                    {
                        original += 6;
                    }
                }
                this.instrumentTab = newString;
                
            }
        }

        /**
        * Fills up the a space in a String[] with '-', making it even
        * @return the string filled to the end with '-'
        */
        public void fillWithNothing()
        {
            for (int j = 0; j < this.getSize(); j++)
            {
               
                String str = this.instrumentTab[j].Substring(0,
                        this.instrumentTab[j].Length);
                for (int i = this.instrumentTab[j].Length; i < this.LIMIT_OF_CHARS; i++)
                {
                    str += "-";
                }
                this.instrumentTab[j] = str;
            }

        }

        /**
     * Trims tab to make  it neat
     */
        public void trim()
        {
            bool isTrimedRelevant = false;
            //Rotates to find a line that has more than 180 chars
            //Goes through each string
            for (int i = 0; i < (this.getSize() / this.instrumentStringNumber); i++)
            {
                String[] trimed = new String[this.instrumentStringNumber];
                int equalizer = 0;


                //I am doing this for all tabs, but for the last one because if there is something extra in the last one,
                //certain things have to change
                for (int z = 0; z < this.instrumentStringNumber; z++)
                {
                    int position = (i * (this.instrumentStringNumber) + z);
                    trimed[z] = "";
                    if (this.instrumentTab[i * (this.instrumentStringNumber) + z].Length > this.LIMIT_OF_CHARS)
                    {
                        //Checks if any of those chars are relevant, and if it is, finds the one that has the highest'size'
                        for (int j = this.LIMIT_OF_CHARS;
                             j < this.instrumentTab[i * (this.instrumentStringNumber) + z].Length; j++)
                        {
                            //Makes checks:
                            if (!this.instrumentTab[i * (this.instrumentStringNumber) + z].Substring(j, 1).Equals("-"))
                            {
                                //Add part in the to the beginning:
                                trimed[z] = this.instrumentTab[i * (this.instrumentStringNumber) + z].
                                        Substring(this.LIMIT_OF_CHARS,
                                                this.instrumentTab[i * (this.instrumentStringNumber) + z].Length - this.LIMIT_OF_CHARS);
                                //If it is make isTrimedRelevant True
                                isTrimedRelevant = true;
                                //Makes equalizer the highest length of substring that has to be added
                                if (equalizer < this.instrumentTab[i * (this.instrumentStringNumber) + z].Length
                                        - this.LIMIT_OF_CHARS)
                                {
                                    equalizer = this.instrumentTab[i * (this.instrumentStringNumber) + z].Length
                                            - this.LIMIT_OF_CHARS;
                                }
                            }
                        }
                    }
                }//End of relevance check
                 //trimmed now has the parts of the string, and isTrimedRelevant shows if it any part of it is important
                 //From here: i = position
                 //           equalizer = length of the largest string in trimed
                 //           trimed = part at end that got removed
                 //           isTrimedRelevand = if any of this information is important


                //Checks if string is relevant
                if (isTrimedRelevant)
                {
                    //Adds another line in case this is the last instance, but there is more stuff that has to be added
                    if (i + 1 == (this.getSize() / this.instrumentStringNumber))
                    {
                        this.addANewTabLine(i + 1);
                    }
                    //Adds the end of last string to the start of this one
                    for (int t = 0; t < this.instrumentStringNumber; t++)
                    {
                        for (int g = trimed[t].Length; g < equalizer; g++)
                        {
                            trimed[t] += "-";
                        }
                        //Adds stuff to the next tab Line
                        //Adds one "-" so it looks better
                        this.instrumentTab[(i + 1) * (this.instrumentStringNumber) + t] =
                               this.instrumentTab[(i + 1) * (this.instrumentStringNumber) + t].Substring(0, 2)
                                + trimed[t] +
                                this.instrumentTab[(i + 1) * (this.instrumentStringNumber) + t].Substring(2,
                                        this.instrumentTab[(i + 1) * (this.instrumentStringNumber) + t].Length - 2);
                    }
                }
                isTrimedRelevant = false;
            }
            //Removes the extra stuff of all tabs
            for (int i = 0; i < this.getSize(); i++)
            {
                this.fillWithNothing();
                this.instrumentTab[i] = this.instrumentTab[i].Substring(0, this.LIMIT_OF_CHARS);
            }
        }

        //INFO:
        /**
         * Gives user an idea of what this code can do
         * @return a String with info
         */
        public static String INFO()
        {
            String str = "Hello!\nThank you for using String editor\nRight now it supports ";
            for(int i = 0; i < instruments.Length; i++)
            {
                str += instruments[i];
                if( i == instruments.Length-1)
                {
                    str += " ";
                }else
                {
                    str += ", ";
                }
            }
            str += "\nYou can use this to easily, and properly edit tabs for instruments\n";

            return str;
        }

        /**
         * shift the string, and sets it as the instrument tab. It shifts verything based from a shifting term
         * precondition: String[] with shift to be changed, and A String that is the base term for the shift
         */
        public void moveNewNotesWithSpace(String [] shiftedTab, String shiftingTerm)
        {
            //Finds Size of shift
            int shiftSize = this.LIMIT_OF_CHARS;
            for(int i = 0; i < shiftedTab.Length; i++)
            {
                if (shiftedTab[i].Length >= shiftSize)
                {
                    shiftSize = shiftedTab[i].Length;
                }
            }
            shiftSize -= this.LIMIT_OF_CHARS;

            int shiftLinePosition = -1;
            int shiftStart = -1;
            for(int i = 0; i< shiftedTab.Length;i++)
            {
                for(int j = 0; j < shiftedTab[i].Length; j++)
                {
                    if(shiftedTab[i].Substring(j,1).Equals(shiftingTerm))
                    {
                        shiftLinePosition = j;
                        shiftStart = (i / this.instrumentStringNumber)*this.instrumentStringNumber;
                    }
                }
            }
            //Adds shift
            String ultimateShift = "";
            for(int i = 0; i<shiftSize;i++)
            {
                ultimateShift += "-";
            }
            if(shiftStart>=0 || shiftLinePosition>=0)
            {
                for(int i = shiftStart; i<shiftStart+this.instrumentStringNumber; i++)
                {
                    String first = shiftedTab[i].Substring(0, shiftLinePosition+1);
                    String second = shiftedTab[i].Substring(shiftLinePosition+1, this.LIMIT_OF_CHARS-shiftLinePosition-1);
                    shiftedTab[i] =  first+ ultimateShift + second;
                }
            }
            //Adds shift to position
            

            this.instrumentTab = shiftedTab;
            this.trim();
        }
    }
}
