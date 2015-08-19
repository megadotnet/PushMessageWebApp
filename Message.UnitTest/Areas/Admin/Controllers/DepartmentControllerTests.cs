using Xunit;
using WebAuth.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAuth.Areas.Admin.Controllers.Tests
{
    public class DepartmentControllerTests
    {
        [Fact()]
        public async Task DetailsTest()
        {
            var departmentController = new DepartmentController();
            var results=await departmentController.Details(0);
            Assert.NotNull(results);
        }

        [Fact()]
        public async void EditTest()
        {
            var departmentController = new DepartmentController();
            var results = await departmentController.Edit(0);
            Assert.NotNull(results);
        }
    }
}