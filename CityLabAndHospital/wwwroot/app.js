var app = angular.module('plunker', []);

app.controller('MainCtrl', function($scope) {
  $scope.name = 'World';
  
       
	  
	  
  $scope.pdfreport1 =function(){
  
  var defaults = {
						separator: ',',
						ignoreColumn: [],
						tableName:'yourTableName',
						type:'csv',
						pdfFontSize:14,
						pdfLeftMargin:20,
						escape:'true',
						htmlContent:'false',
						consoleLog:'false'
				};
				
					var doc = new jsPDF('p','pt', 'a4', true);
					doc.setFontSize(defaults.pdfFontSize);
					
					// Header
					var startRowPosition = 20;
					var startColPosition=defaults.pdfLeftMargin;
					
					var startRowPosition = 20;
					var startColPosition=defaults.pdfLeftMargin;
					//doc.text(startColPosition,20, "Heading");
					//startColPosition = startColPosition+ 20;
					doc.text(startColPosition,startRowPosition, "Heading");
					
					
					// Output as Data URI
					doc.output('datauriNew');
					
  }
	  
	  
        
      $scope.reportData = [
                     {
                         "EmployeeID": "1234567",
                         "LastName": "Lastname",
                         "FirstName": "First name",
                         "Salary": 1000
                     },
                     {
                         "EmployeeID": "11111111",
                         "LastName": "Lastname 1",
                         "FirstName": "First name 1",
                         "Salary": 2000
                     },
                     {
                         "EmployeeID": "222222222",
                         "LastName": "Lastname 2",
                         "FirstName": "First name 2",
                         "Salary": 3000
                     },
                     {
                         "EmployeeID": "333333333",
                         "LastName": "Lastname 3",
                         "FirstName": "First name 3",
                         "Salary": 4000
                     }
            ];
        
  
  
  
  
  
  
  
  
  
  
  
  
  
                
  
});

