using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.ServiceModel.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.KatomBook;

public partial class api_upload : System.Web.UI.Page
{
    private static Random random = new Random();
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("/Login.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        if (Request.Files["UploadedFile"] != null)
        {
            HttpPostedFile MyFile = Request.Files["UploadedFile"];
            //Setting location to upload files
            string TargetLocation = Server.MapPath("/ProfilePics/");
            try
            {
                if (MyFile.ContentLength > 0)
                {
                    if(MyFile.FileName.Split('.')[1] == "jpeg" || MyFile.FileName.Split('.')[1] == "jpg" || MyFile.FileName.Split('.')[1] == "png")
                    {
                        if (MyFile.ContentLength < 5000000)
                        {
                            //Determining file name. You can format it as you wish.
                            string FileName = RandomString(8) + "." + MyFile.FileName.Split('.')[1];
                            string OldFile = "";
                            DataTable user = MyAdoHelper.ExecuteDataTable("SELECT * FROM `tblUsers` WHERE `fldIndex` = " + Request.Form["id"] + ";");
                            OldFile = user.Rows[0]["fldProfilePic"].ToString();
                   
                            if (System.IO.File.Exists("C:\\Users\\tomce\\Desktop\\KatomBook_School\\ProfilePics" + OldFile))
                            {
                                System.IO.File.Delete("C:\\Users\\tomce\\Desktop\\KatomBook_School\\ProfilePics" + OldFile);
                                Response.Write(OldFile);
                            }
                               
                            MyAdoHelper.DoQuery("UPDATE `tblUsers` SET `fldProfilePic` = '" + FileName + "' WHERE `fldIndex` = " + Request.Form["id"] + ";");
                            //Determining file size.
                            int FileSize = MyFile.ContentLength;
                            //Creating a byte array corresponding to file size.
                            byte[] FileByteArray = new byte[FileSize];
                            //Posted file is being pushed into byte array.
                            MyFile.InputStream.Read(FileByteArray, 0, FileSize);
                            //Uploading properly formatted file to server.
                            MyFile.SaveAs(TargetLocation + FileName);
                            Response.Redirect("/profile.aspx", false);
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                        }
                        else
                        {
                            Response.Write("File Too Big!!!");
                        }
                    }
                    else
                    {
                        Response.Write("Invalid File Name!!!");
                    }
                }
            }
            catch (Exception BlueScreen)
            {
                //Handle errors
            }
        }
    }
}