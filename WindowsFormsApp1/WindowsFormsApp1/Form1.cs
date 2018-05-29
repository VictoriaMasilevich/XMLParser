using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            string KeyValue;
            string FilePath = textBox1.Text.ToString();
            try
            {
                using (XmlTextReader XmlReader = new XmlTextReader(FilePath))
                {
                    Dictionary<string, int> TagDictionary = new Dictionary<string, int>();
                    TagDictionary.Add("no-role", 0);
                    while (XmlReader.Read())
                    {
                        if (XmlReader.NodeType == XmlNodeType.Element)
                        {
                            if (XmlReader.Name == "user" && (XmlReader.GetAttribute("role")!=null))
                            {
                                KeyValue = XmlReader.GetAttribute("role");
                                if (KeyValue != null)
                                {
                                    if (!TagDictionary.ContainsKey(KeyValue))
                                    {
                                        TagDictionary.Add(KeyValue, 1);
                                    }
                                    else
                                    {
                                        TagDictionary[KeyValue] = ++TagDictionary[KeyValue];
                                    }
                                }
                            }
                            else
                            {
                                if (XmlReader.Name == "user")
                                {
                                    TagDictionary["no-role"] = ++TagDictionary["no-role"];
                                }
                            }
                        }

                    }
                    foreach (KeyValuePair<string, int> output in TagDictionary)
                    {
                        textBox2.Items.Add(output.Key + ": " + output.Value);
                    }

                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Файл не найден!");
            }
            catch (XmlException)
            {
                MessageBox.Show("Ошибка считывания!");
            }
            catch
            {
                MessageBox.Show("Неверный путь!");
            }
        }
    }
}
