using System.Data;
using System.Data.OleDb;
using System.Web;

namespace com.KatomBook
{
    public class MyAdoHelper
    {
        public static OleDbConnection ConnectToDb(string fileName)
        {
            string path = HttpContext.Current.Server.MapPath("App_Data/").Replace("api", ""); //מיקום מסד בפורוייקט
            path += fileName;
            //string path = HttpContext.Current.Server.MapPath("App_Data/" + fileName);//מאתר את מיקום מסד הנתונים מהשורש ועד התקייה בה ממוקם המסד
            string connString =
                "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path; //נתוני ההתחברות הכוללים מיקום וסוג המסד
            OleDbConnection conn = new OleDbConnection(connString);
            return conn;
        }

        /// <summary>
        /// To Execute update / insert / delete queries
        ///  הפעולה מקבלת שם קובץ ומשפט לביצוע ומבצעת את הפעולה על המסד
        /// </summary>

        public static void DoQuery(string sql) //הפעולה מקבלת שם מסד נתונים ומחרוזת מחיקה/ הוספה/ עדכון
            //ומבצעת את הפקודה על המסד הפיזי
        {
            string fileName = "KatomBook_DB.mdb";
            OleDbConnection conn = ConnectToDb(fileName);
            conn.Open();
            OleDbCommand com = new OleDbCommand(sql, conn);
            com.ExecuteNonQuery();
            com.Dispose();
            conn.Close();

        }


        /// <summary>
        /// To Execute update / insert / delete queries
        ///  הפעולה מקבלת שם קובץ ומשפט לביצוע ומחזירה את מספר השורות שהושפעו מביצוע הפעולה
        /// </summary>
        public int RowsAffected(string fileName, string sql) //הפעולה מקבלת מסלול מסד נתונים ופקודת עדכון
            //ומבצעת את הפקודה על המסד הפיזי
        {

            OleDbConnection conn = ConnectToDb(fileName);
            conn.Open();
            OleDbCommand com = new OleDbCommand(sql, conn);
            int rowsA = com.ExecuteNonQuery();
            conn.Close();
            return rowsA;
        }

        /// <summary>
        /// הפעולה מקבלת שם קובץ ומשפט לחיפוש ערך - מחזירה אמת אם הערך נמצא ושקר אחרת
        /// </summary>
        public static bool
            IsExist(string sql) //הפעולה מקבלת שם קובץ ומשפט בחירת נתון ומחזירה אמת אם הנתונים קיימים ושקר אחרת
        {
            string fileName = "KatomBook_DB.mdb";
            OleDbConnection conn = ConnectToDb(fileName);
            conn.Open();
            OleDbCommand com = new OleDbCommand(sql, conn);
            OleDbDataReader data = com.ExecuteReader();
            bool found;
            found = (bool) data.Read(); // אם יש נתונים לקריאה יושם אמת אחרת שקר - הערך קיים במסד הנתונים
            conn.Close();
            return found;

        }
        //רועי

        public static DataTable ExecuteDataTable(string sql)
        {
            string fileName = "KatomBook_DB.mdb";
            OleDbConnection conn = ConnectToDb(fileName);
            conn.Open();
            OleDbDataAdapter tableAdapter = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            tableAdapter.Fill(dt);
            conn.Close();
            return dt;
        }


        public void ExecuteNonQuery(string fileName, string sql)
        {
            OleDbConnection conn = ConnectToDb(fileName);
            conn.Open();
            OleDbCommand command = new OleDbCommand(sql, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static string
            printDataTable(string fileName,
                string sql) //הפעולה מקבלת שם קובץ ומשפט בחירת נתון ומחזירה אמת אם הנתונים קיימים ושקר אחרת
        {


            DataTable dt = ExecuteDataTable(sql);

            string printStr = "<table border='1'>";

            foreach (DataRow row in dt.Rows)
            {
                printStr += "<tr>";
                foreach (object myItemArray in row.ItemArray)
                {

                    printStr += "<td>" + myItemArray.ToString() + "</td>";
                }

                printStr += "</tr>";
            }

            printStr += "</table>";

            return printStr;
        }

    }
}