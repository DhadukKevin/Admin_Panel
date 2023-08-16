using Demo_Areas.Areas.LOC_City.Models;
using Demo_Areas.Areas.LOC_Country.Models;
using Demo_Areas.Areas.LOC_State.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Demo_Areas.Areas.LOC_City.Controllers
{
    [Area("LOC_City")]
    [Route("LOC_City/[controller]/[action]")]
    public class LOC_CityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        #region CONNECTION STRING
        private readonly IConfiguration Configuration;
        public LOC_CityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region SELECT ALL
        public IActionResult LOC_CityList()
        {
            string connectionStr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            SqlCommand objCmd = connection.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_City_SelectAll";
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            return View("LOC_CityList",dt);
        }
        #endregion

        #region DELETE
        public IActionResult Delete(int CityID)
        {
            string connectionStr = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            SqlCommand objCmd = connection.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_City_DeleteByPK";
            objCmd.Parameters.AddWithValue("@CityID", CityID);
            objCmd.ExecuteNonQuery();
            return RedirectToAction("LOC_CityList");
        }
        #endregion

        #region ADD
        public IActionResult Add(int? CityID)
        {
            #region ComboBox

            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");

            DataTable dt1 = new DataTable();
            SqlConnection conn1 = new SqlConnection(connectionstr);
            conn1.Open();
            SqlCommand objCmd1 = conn1.CreateCommand();
            objCmd1.CommandType = CommandType.StoredProcedure;
            objCmd1.CommandText = "PR_Country_ComboBox";
            SqlDataReader objSDR1 = objCmd1.ExecuteReader();
            dt1.Load(objSDR1);

            List<LOC_CountryDropDownModel> list = new List<LOC_CountryDropDownModel>();

            foreach (DataRow dr in dt1.Rows)
            {
                LOC_CountryDropDownModel vlst = new LOC_CountryDropDownModel();
                vlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                vlst.CountryName = dr["CountryName"].ToString();
                list.Add(vlst);
            }

            ViewBag.CountryList = list;

            #endregion

            List<LOC_StateDropDownModel> list1 = new List<LOC_StateDropDownModel>();
            ViewBag.StateList = list1;

            if (CityID != null)
            {
                SqlConnection connection = new SqlConnection(connectionstr);
                connection.Open();
                SqlCommand objCmd = connection.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_City_SelectByPk";
                objCmd.Parameters.AddWithValue("@CityID", CityID);
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();

                dt.Load(objSDR);

                LOC_CityModel modelLOC_City = new LOC_CityModel();

                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_City.CityName = (string)dr["CityName"];
                    modelLOC_City.CityID = Convert.ToInt32(dr["CityID"]);
                    modelLOC_City.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_City.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_City.CityCode = (string)dr["CityCode"];
                }
                return View("LOC_CityAdd", modelLOC_City);
            }
            else
            {
                return View("LOC_CityAdd");
            }
        }
        #endregion

        #region SAVE
        public IActionResult Save(LOC_CityModel modelLOC_City)
        {
            string connectionStr = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            SqlCommand objCmd = connection.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            if (modelLOC_City.CityID == null)
            {
                objCmd.CommandText = "PR_City_Insert";
            }
            else
            {
                objCmd.CommandText = "PR_City_UpdateByPk";
                objCmd.Parameters.AddWithValue("@CityID", modelLOC_City.CityID);
            }

            objCmd.Parameters.AddWithValue("@CityName", modelLOC_City.CityName);
            objCmd.Parameters.AddWithValue("@StateID", modelLOC_City.StateID);
            objCmd.Parameters.AddWithValue("@CountryID", modelLOC_City.CountryID);
            objCmd.Parameters.AddWithValue("@CityCode", modelLOC_City.CityCode);

            objCmd.ExecuteNonQuery();

            connection.Close();

            return RedirectToAction("LOC_CityList");
        }
        #endregion

        #region SEARCH BY NAME
        public IActionResult LOC_CitySearchByName(string? CityName)
        {
            string connectionStr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            SqlCommand objCmd = connection.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_City_SelectByCityName";
            objCmd.Parameters.AddWithValue("@CityName", CityName);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            return View("LOC_CityList", dt);
        }
        #endregion

        #region Dropdownfill 
        public IActionResult DropdownByCountry(int CountryID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            //PrePare a connection
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_State_ComboBoxbyCountryId";
            objCmd.Parameters.AddWithValue("@CountryID", CountryID);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            conn.Close();

            List<LOC_StateDropDownModel> list = new List<LOC_StateDropDownModel>();

            foreach (DataRow dr in dt.Rows)
            {
                LOC_StateDropDownModel vlst = new LOC_StateDropDownModel();
                vlst.StateID = Convert.ToInt32(dr["StateID"]);
                vlst.StateName = dr["StateName"].ToString();
                list.Add(vlst);
            }

            var vModel = list;
            return Json(vModel);
        }
        #endregion
    }
}
