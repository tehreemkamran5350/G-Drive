<!DOCTYPE html>
<html lang="en">
<head>
<title>admin</title>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<style>
* {
    box-sizing: border-box;
    font-family: Arial, Helvetica, sans-serif;
}

body {
  margin: 0;
  font-family: Arial, Helvetica, sans-serif;
}


.topnav {
    overflow: hidden;
    background-color: #333;
}

/* Style the topnav links */
.topnav a {
    float: left;
    display: block;
    color: #f2f2f2;
    text-align: center;
    padding: 14px 16px;
    text-decoration: none;
}

/* Change color on hover */
.topnav a:hover {
    background-color: #ddd;
    color: black;
}

/* Style the content */
.content {
    background-color: #ddd;
    padding: 10px;
    height: 200px; 
}


</style>
</head>
<body>

<div class="topnav">
		<a href="admin.php" >Home</a>
	  <a href="User_Mangement.php" >User Management</a>
	     <a href="Role_Mangement.php" >Role Management</a>
	    <a href="Permission_Mangement.php" >Permissions Management</a>
	   <a href="Role_Permission.php">Role-Permissions Assignment</a>
	<a href="User_Role.php">User-Role Assignment</a>
	   <a href="login.php" >Logout</a>

</div>


  <h1>Welcome Admin</h1>


</body>
</html>
