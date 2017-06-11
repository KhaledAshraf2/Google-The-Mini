using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace SSE
{
    public static class Functions
    {
        public static string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);//a dynamic directory to get the path of files which are located beside the .exe
        public static DirectoryInfo di = new DirectoryInfo(dir + @"\Files");
        public static FileInfo[] File = di.GetFiles("*.txt").ToArray();
        public static Dictionary<string, int>[] Hashmap = new Dictionary<string, int>[File.Length]; //unsorted map InverseIndexing map, Each file has it's own dictionary and it's own Inverseindex
        public static SortedDictionary<int, SortedDictionary<int,Listt<int>>> Batches = new SortedDictionary<int, SortedDictionary<int, Listt<int>>>(); //map<Number of Unique words in each file, <Total number of occurence,List of number of files>>
        public static SortedDictionary<Tuple<int,int>, Listt<string>> WordOccurenceInFiles = new SortedDictionary<Tuple<int,int>, Listt<string>>();//map< <number of file,number of occurence>, List of words repeated in this specific key>
        public static Listt<string> FirstLines = new Listt<string>();//List contains each 3 lines of every file
        public static SortedSet<string> QueryWords=new SortedSet<string>();//a set made to retrieve the query and split it into words
        public static Listt<string> body1 = new Listt<string>();
        public static Listt<string> body2 = new Listt<string>();
        public static Listt<string> body3 = new Listt<string>();
        public static Listt<string> filenames = new Listt<string>();
        public static int counter=0;
        public static string RemoveSpecialCharacters(this string str)//A simple function to remove delimeters in the word like comma dot and other characters 
        {
            string delimiters = ".,!?(){}\"-+&%;<>/'";
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                bool flag = true;
                foreach (char s in delimiters)
                {
                    if (c == s)
                    { flag = false; break; }
                }
                if (flag)
                    sb.Append(c);
            }
            return sb.ToString();
        }


        public static void InverseIndexing()
        {
   
            string FileName = dir + @"\DelimitersWords.txt"; ;//The file of word delimiters like and or which where what and so on
            Listt<string> DelimiterW = new Listt<string>();//a list to store the word delimiters
            FileStream fsIn1 = new FileStream(FileName, FileMode.Open,FileAccess.Read, FileShare.Read);
            using (StreamReader sr = new StreamReader(fsIn1, Encoding.UTF8, true))
            {
                // While not at the end of the file, read lines from the file.
                while (sr.Peek() > -1)
                {
                    DelimiterW.add(sr.ReadLine());
                }
            }
            ////////////////////////////////////////////////////////
            
            int NumberOfFiles = 0;
            while(NumberOfFiles<File.Length)
            {
                Hashmap[NumberOfFiles] = new Dictionary<string, int>();//Intializing an object for each index of the array of dictionary
                FileName =dir + @"\Files\"+File[NumberOfFiles].ToString();//directory + name of file + .txt for a full directory path of each file
                string FirstThreeLines="",Line;
                int TempCount = 0;
                FileStream fsIn2 = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                using (StreamReader sr = new StreamReader(fsIn2, Encoding.UTF8, true))
                {
                    // While not at the end of the file, read lines from the file.
                    while (sr.Peek() > -1)
                    {
                        Line = sr.ReadLine();
                        TempCount++;
                        if (TempCount <= 3)
                        {
                            FirstThreeLines += Line + Environment.NewLine;//writing the first lines in this string
                            if(TempCount==3)
                            {
                                FirstLines.add(FirstThreeLines);//Pushing the whole string into the first index of the list which means the first file and so on
                            }
                        }
                        Line = Line.RemoveSpecialCharacters();
                            var words = Line.Split(' ');//splitting the line into an array of words 
                        foreach (string word in words)//loop on each word in the var words
                        {
                            bool Flag = true;
                            string temp=word.ToLower();//change the word to lowercase
                            //foreach (string delim in DelimiterW)//loop on the word delimeters to check whether the word is a word delimiter or not
                            for(int i=1;i<=DelimiterW.count();i++)
                            {
                                string delim = DelimiterW.getvalue(i);
                                if (temp == delim)
                                { Flag = false; break; }
                            }
                            
                            if(Flag)//If the word isn't a delimiter
                            {

                                if(Hashmap[NumberOfFiles].ContainsKey(temp))//If this dictionary contains that key, get the value and add 1 on it and then put it back again
                                {
                                   int value;
                                   Hashmap[NumberOfFiles].TryGetValue(temp, out value);
                                   value++;
                                    Hashmap[NumberOfFiles][temp] = value;
                                }
                                else//Add the word to the dictionary with a value of one
                                {
                                    Hashmap[NumberOfFiles].Add(temp, 1);
                                }
                            }
                        }
                    }
                }

                NumberOfFiles++;
            }
        }

        public static void GetQuery(string word)//Gets the text written and splits it into words and adds it into the sorted set
        {
            var wrd = word.Split(' ');
            foreach(string x in wrd)
            {
                if(x!="")
                QueryWords.Add(x);
            }
        }

        public static void BatchesFiller()
        {
            int NumberOfFile = 0;
            while(NumberOfFile<File.Length)
            {
                Listt<string> TempWords = new Listt<string>();
                int UniqueWordsInFile = QueryWords.Count(),TotalNumberOfOccurences = 0;
                foreach(string it in QueryWords)//Loops on each word in the set of words
                {
                    if(!Hashmap[NumberOfFile].ContainsKey(it))//If the word isn't in the file
                    {
                        UniqueWordsInFile--;
                    }
                    else
                    {
                        TotalNumberOfOccurences += Hashmap[NumberOfFile][it];//Add the number of occurence of this word to the total number
                        TempWords.add(it);//add the word to the temp List
                    }
                }
                /*Unlike C++, Dictionaries and maps and everything with a key and value, You can't access the key if it doesn't exist as it doesn't create a key on it's own
                So i have to check whether the key exists or not and add the key if it doesn't exist, And you have to intialize and object for every list inside a dictionary
                Or for every dictionary inside a dictioanry or a Tuple inside a dictionary and so on*/
                if(Batches.ContainsKey(UniqueWordsInFile))
                {
                    if(Batches[UniqueWordsInFile].ContainsKey(TotalNumberOfOccurences))
                    {
                        Batches[UniqueWordsInFile][TotalNumberOfOccurences].add(NumberOfFile);
                    }
                    else
                    {
                        Batches[UniqueWordsInFile].Add(TotalNumberOfOccurences,null);//Add the key first and set the value to null
                        Batches[UniqueWordsInFile][TotalNumberOfOccurences] = new Listt<int>();//Intialize the value which is a list
                        Batches[UniqueWordsInFile][TotalNumberOfOccurences].add(NumberOfFile);//Add the value inside the list
                    }
                }
                else
                {
                    Batches.Add(UniqueWordsInFile,null);//Add the key to the main dictionary
                    Batches[UniqueWordsInFile]= new SortedDictionary<int,Listt<int>>();//Intialize the value of the main dictionary which is a dictionary
                    Batches[UniqueWordsInFile].Add(TotalNumberOfOccurences, null);//Give it a key and set value to null
                    Batches[UniqueWordsInFile][TotalNumberOfOccurences] = new Listt<int>();//Intialize the value of the second dictionary which is a list
                    Batches[UniqueWordsInFile][TotalNumberOfOccurences].add(NumberOfFile);//Add the value to the list
                }
                
                    //new Tuple<type,type>(value,value)
                    WordOccurenceInFiles.Add(new Tuple<int, int>(NumberOfFile, TotalNumberOfOccurences), null);
                    WordOccurenceInFiles[new Tuple<int, int>(NumberOfFile, TotalNumberOfOccurences)] = new Listt<string>();
                    WordOccurenceInFiles[new Tuple<int, int>(NumberOfFile, TotalNumberOfOccurences)] = TempWords;
                
                NumberOfFile++;
            }
        }

        public static void Output()
        {
            body1.clear();
            body2.clear();
            body3.clear();
            filenames.clear();
            counter = 0;
            foreach (var FirstIt in Batches.Reverse())//Since the It's a sorted dictionary so it sorts in ascending order and we want in desending order so we iterate in reverse
            {
                foreach(var SecondIt in Batches[FirstIt.Key].Reverse())//Same thing but for the dictionary inside the batches dictionary
                {
                    int i = 0;
                    while(i<SecondIt.Value.count())//i<Total number of files in the biggest total number of occurence in the biggest batch EX: Query is 7 words, Batch 7, 120Times in 5 different files
                    {
                        if (FirstIt.Key == 0)//Since there is no unique word in this file ,there is no zero batches
                        {
                            i++; continue;
                        }
                        body1.add(" Contains " + FirstIt.Key.ToString() + " words :-");
                        filenames.add(File[SecondIt.Value.getvalue(i)].ToString());
                        SortedSet<Tuple<int, string>> Sorted = new SortedSet<Tuple<int, string>>();//<Number of occurence,The word> To sort the words that we output in their number of occurence in each file
                        counter++;
                        int j = 0;
                        while (j <FirstIt.Key)//j<Number of words in this dictionary 
                        {                   
                            int NumberOfcurrentFile = SecondIt.Value.getvalue(i);
                            string WordO = WordOccurenceInFiles[new Tuple<int, int>(NumberOfcurrentFile, SecondIt.Key)].getvalue(j);
                            int WordOcc = Hashmap[NumberOfcurrentFile][WordO];
                            Sorted.Add(new Tuple<int, string>(WordOcc,WordO));

                            j++;
                        }
                        string temp = "       ";
                        foreach(var yt in Sorted.Reverse())//Itereate on the sorted map in reverse as well and then add to the body
                        {
                            temp += yt.Item2.ToString() + " Has been repeated " + yt.Item1.ToString()+" times         ";
                        }
                        body2.add(temp);
                        body3.add(FirstLines.getvalue(SecondIt.Value.getvalue(i)));
                    i++;
                    }
                }
            }
        }
    }
}
