


var SimpleListModel = function (items) {

    var self = this;

    self.Name = ko.observable();
    self.id = ko.observable();
    self.Department = ko.observable();
    self.Gender = ko.observable();
    self.Salary = ko.observable();

    self.Emp = {
        Name: self.Name,
        Department: self.Department,
        Gender: self.Gender,
        Salary: self.Salary,
        ID: self.id
    }

    self.listEmp = ko.observableArray();
    self.DisplayMainData = ko.observable(true);
    self.EmployeeFormTitle = ko.observable(null);

    self.isAdd = ko.observable();

    // All Employee List

    $.ajax({
        type: 'GET',
        url: './EmployeeList',
        dataType: "json",
        success: function (result) {
            self.listEmp(result);
        }
    });

    self.NewEmployee = function () {
        self.EmployeeFormTitle('New Employee');
        //New data.....
        $.ajax({
            type: 'GET',
            url: './GetEmployeeData',
            dataType: "json",
        }).done(function (data) {
            self.EmployeeFormTitle(true);
            self.isAdd(true);
        })
        self.DisplayMainData(false);
        self.isAdd(true);
    }

    ///////////////// Edit Employee Get Method ////////////////////////////
    self.EditEmployee = function (Emp) {
        self.EmployeeFormTitle(true);
        
        $.ajax({
            type: 'GET',
            url: './GetEmployeeData/' + Emp.ID
        }).done(function (data) {
            self.id(data.ID);
            self.Name(data.Name);
            self.Gender(data.Gender);
            self.Salary(data.Salary);
            self.Department(data.Department);
            self.EmployeeFormTitle('Edit Employee : ' + Emp.Name);
            self.DisplayMainData(false);
            self.isAdd(false);
        }).fail();
    }

    ////////// Edit Employee Post Method   //////////////////////
    
    self.UpdateEmployee = function () {

            $("#empform").removeData("validator");
            $("#empform").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse("#empform");

            if ($("#empform").valid()) {
            // Submit the data.....
            $.ajax({
                type: 'POST',
                url: './SaveEmployeeData',
                data: {
                    ID: self.id,
                    Name: self.Name,
                    Gender: self.Gender,
                    Salary: self.Salary,
                    Department: self.Department,
                    isAdd: self.isAdd()
                },
                dataType: "json",
                success: function (result) {
                    if (result.Success) {
                        self.DisplayMainData(false);
                        self.addEmployee(null);
                    }
                }
            }).fail();
        }
    }
    self.DeleteEmployee = function (Emp) {

        if (confirm('Are you sure to Delete "' + Emp.Name + '" Employee ??')) {

            $.ajax({
                url: './DeleteEmployeeData',
                type: 'POST',
                data: { id: Emp.ID },
                success: function (data) {
                    // self.Products.remove(Product);
                    alert(Emp.Name + ' has deleted!');
                }
            }).fail(
            function (xhr, textStatus, err) {

                self.DisplayMainData(true);
                alert(err);
            });
        }

    }

    self.Cancel = function () {
        self.DisplayMainData(true);
        self.EmployeeFormTitle(false);
    }



    //////// Get All Departments ///////
    //self.DID = ko.observable();
    //self.DepartmentName = ko.observable();
    //self.Location = ko.observable();
    //self.DepartmentHead = ko.observable();

    self.Departments = ko.observableArray();

    //self.Dept = {
    //    ID: self.DID,
    //    DepartmentName: self.DepartmentName,
    //    Location: self.Location,
    //    DepartmentHead: self.DepartmentHead
    //}


    self.DepartmentDetails = function (Emp) {
        $.ajax({
            type: 'GET',
            url: './GetDepartments/' + Emp.Department
        }).done(function (data) {
            self.Departments.removeAll();
            self.Departments.push(data);
           
            $("#department").modal({
                "backdrop": "static",
                "keyboard": true,
                "show": true // this parameter ensures the modal is shown immediately
            });
        }).fail();
    }


}