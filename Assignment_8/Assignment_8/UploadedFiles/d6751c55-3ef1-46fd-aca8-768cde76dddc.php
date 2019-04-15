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
    <script src="SecurityManager.js"></script>
    <script>
        function Main()
        {
            
            var sp = document.getElementById("sp");
           
            var b = document.createElement('br');
            var login = localStorage.getItem("username");
            var u_role = SecurityManager.GetAllUserRoles();
            var u_per = SecurityManager.GetAllRolePermissions();
            var c = 1;
            for(var i=0;i<u_role.length;i++)
            {

                if (u_role[i].user == login)
                {
                    sp.innerHTML = sp.innerHTML+ c + ". Role:   ";
                    console.log(u_role[i].role);
                    c++;                  
                    sp.innerHTML=sp.innerHTML + u_role[i].role;
                    sp.appendChild(b);
                   
                    sp.innerHTML = sp.innerHTML + "Permissions:   ";
                    sp.appendChild(b);
                    debugger;
                    for (var j = 0; j < u_per.length; j++) {
                        if (u_per[j].role == u_role[i].role) {
                            
                            console.log(u_per[j].permission);
                            sp.innerHTML = sp.innerHTML +"---------------------" + u_per[j].permission;
                            sp.appendChild(b);
                          
                        }
                    }
                   
                }          
            }
            
            
        }
    </script>
</head>
<body onload="Main();">

    <div class="topnav">
        <a href="user.php">Home</a>
        
        <a href="login.php">Logout</a>

    </div>
  

    <h1 style="background-color:gainsboro;" align="center">Welcome User</h1>
     <form align=center>       
         <span id="sp"> </span>
         </form>


</body>
</html>
