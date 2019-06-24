using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using System.Collections.Generic;
using System.Linq;
using A = DocumentFormat.OpenXml.Drawing;
using Text = DocumentFormat.OpenXml.Drawing.Text;
using TextBody = DocumentFormat.OpenXml.Drawing.TextBody;

namespace DocumentGenerator.PowerPoint
{
    public class TableGeneratorPowerPoint
    {
        const string existingTableFile = "UpdateExistingTable";
        const int rowsPerSlide = 11;

        public void Run<T>(IEnumerable<T> data, string title)
        {



            // Open the presentation as read-only.
            using (PresentationDocument presentationDocument =
                PresentationDocument.Open(@"C:\Users\Faisal Saleem\source\repos\FaisalLearningProjectMVC\Templates\UpdateExistingTable6.pptx", true))
            {

                // Get a PresentationPart object from the PresentationDocument object.
                PresentationPart presentationPart = presentationDocument.PresentationPart;

                // Get a Presentation object from the PresentationPart object.
                Presentation presentation = presentationPart.Presentation;


                if (presentation.SlideIdList != null)
                {
                    // Get the collection of slide IDs from the slide ID list.
                    DocumentFormat.OpenXml.OpenXmlElementList slideIds =
                        presentation.SlideIdList.ChildElements;

                    int slideIndex = 0;

                    // If the slide ID is in range...
                    if (slideIndex < slideIds.Count)
                    {
                        // Get the relationship ID of the slide.
                        string slidePartRelationshipId = (slideIds[slideIndex] as SlideId).RelationshipId;

                        // Get the specified slide part from the relationship ID.
                        SlidePart slidePart =
                            (SlidePart)presentationPart.GetPartById(slidePartRelationshipId);
                        if (slidePart.Slide != null)
                        {
                            A.Table table = slidePart.Slide.Descendants<A.Table>().First();

                            A.TableRow tableRow1 = table.Elements<A.TableRow>().ElementAt(1);

                            A.TableCell tableCell1 = tableRow1.Elements<A.TableCell>().ElementAt(1);


                            A.TextBody textBody1 = tableCell1.GetFirstChild<A.TextBody>();

                            A.Paragraph paragraph1 = textBody1.GetFirstChild<A.Paragraph>();

                            A.EndParagraphRunProperties endParagraphRunProperties1 = paragraph1.GetFirstChild<A.EndParagraphRunProperties>();
                            A.Run run1 = new A.Run();
                            A.RunProperties runProperties1 = new A.RunProperties() { Language = "en-US", AlternativeLanguage = "zh-CN", Dirty = false };
                            A.Text text1 = new A.Text();


                            text1.Text = "John";

                            run1.Append(runProperties1);
                            run1.Append(text1);
                            paragraph1.InsertBefore(run1, endParagraphRunProperties1);
                            endParagraphRunProperties1.Dirty = false;


                            CreateNewRow(table);

                        }
                    }
                }

                presentationPart.Presentation.Save();

            }


            //AppConfiguration appConfig = new AppConfiguration();

            //// save the document 
            //string newTitle = $"{ title} {DateTime.Now.ToString().Replace("/", "").Replace(":", "")}";
            //var filePath = appConfig.OutputFolderDirectory + Path.DirectorySeparatorChar + newTitle;

        }

        private void CreateNewRow(Table table)
        {
            TableRow tableRow1 = new TableRow() { Height = 370840L };

            TableCell tableCell1 = new TableCell();

            TextBody textBody1 = new TextBody();
            BodyProperties bodyProperties1 = new BodyProperties();
            ListStyle listStyle1 = new ListStyle();

            Paragraph paragraph1 = new Paragraph();

            Run run1 = new Run();
            RunProperties runProperties1 = new RunProperties() { Language = "en-US", Dirty = false };
            Text text1 = new Text();
            text1.Text = "Column1";

            run1.Append(runProperties1);
            run1.Append(text1);
            EndParagraphRunProperties endParagraphRunProperties1 = new EndParagraphRunProperties() { Language = "en-US", Dirty = false };

            paragraph1.Append(run1);
            paragraph1.Append(endParagraphRunProperties1);

            textBody1.Append(bodyProperties1);
            textBody1.Append(listStyle1);
            textBody1.Append(paragraph1);
            TableCellProperties tableCellProperties1 = new TableCellProperties();

            tableCell1.Append(textBody1);
            tableCell1.Append(tableCellProperties1);

            table.Append(tableRow1);
        }




    }
}
