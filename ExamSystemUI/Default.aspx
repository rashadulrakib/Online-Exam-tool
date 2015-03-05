<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN"
    "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" >
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="application/xhtml+xml; charset=UTF-8" />
<meta name="Author" content="Stu Nicholls" />
<title>Exam System</title>
<style type="text/css">

		#tblMain
        {
            border:1px solid #296488
        }
        #tdMain
        {
            border:none
        }
				
body {
  margin:0;
  border:0;
  padding:0;
  height:100%; 
  max-height:100%; 
  overflow: hidden; 
  
  }

#header {
  position:absolute; 
  top:0; 
  left:0; 
  width:100%; 
  height:85px; 
  overflow:auto; 
  }

#footer {
  position:absolute; 
  bottom:0; 
  left:0;
  width:100%; 
  height:30px; 
  overflow:auto; 
  text-align:right; 
  }

#contents {
  position:fixed; 
  top:80px;
  left:0;
  bottom:30px; 
  right:0; 
  overflow:auto; 

  }

  

/* for internet explorer */

* html body {
  padding:80px 0 30px 0; 
  
  }

* html #contents {
  height:100%; 
  width:100%; 
  }
  


</style>


</head>
<body>

<div id="header">
    <img src="Images/HeaderFooter/header.bmp" width="100%" height="80px"/></div>
<div id="footer">
    <img src="Images/HeaderFooter/footer.bmp" width="100%" height="30px"/></div>

<div id="contents">

   <table width="100%" cellSpacing="0" cellPadding="0" border="0" style="margin:0">
        <tr>
            <td>
                <br />
            </td>
        
        </tr>
        <tr>
            <td style="background-color: whitesmoke; height:25px ">
            
            </td>
        
        </tr>
        <tr>
            <td>
            <br />
            <br />
               <center>
		    <div>

			<form runat="server" id="form1">
			<table cellpadding="0" cellspacing="0" style=" margin:0" border="0">
			    <tr>
			        <td style="text-align:left">
                        <asp:ValidationSummary ID="vsSystemUser" runat="server" HeaderText="Following fields missing or invalid" />
                    </td>
			    </tr>
			    <tr>
			        <td style="border:none;" align="left">
			            <asp:Label ID="lbl_Error" runat="server" ></asp:Label></td>
			        
			    </tr>
			    <tr>
			        <td style="border:none;" align="left">
			            <table id="tblMain" cellpadding="0" cellspacing="0" style=" margin:0" border="1" bgcolor="aliceblue">
                <tr>
                    <td id="tdMain">
                        <table cellpadding="0" cellspacing="0" style=" margin:0" border="0">
                            <tr>
                                <td colspan="1" style="vertical-align: top; width: 5px; height: 20px; text-align: center" bgcolor="#b0e0e6">
                                </td>
                                <td colspan="3" style="vertical-align: top; height: 20px;" align="left" valign="top" bgcolor="#b0e0e6">
                                    <asp:Label ID="Label3" runat="server" Text="System User Login" Font-Bold="True"></asp:Label>
                                </td>
                                <td colspan="1" style="vertical-align: top; width: 10px; height: 20px; text-align: center" bgcolor="#b0e0e6">
                                </td>
                            </tr>
            <tr>
                <td colspan="1" style="vertical-align: top; height: 5px; text-align: center;width:5px">
                </td>
                <td colspan="3" style="vertical-align: top; height:5px" align="left">
                    
                <td colspan="1" style="vertical-align: top; height: 5px; text-align: center;width:10px">
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left;width:5px">
                </td>
                <td style="vertical-align: top; text-align: left">
                    <asp:Label ID="Label1" runat="server" Style="text-align: left" Text="Login Name"></asp:Label></td>
                <td style="vertical-align: top; text-align: left;width:5px">
                </td>
                <td style="vertical-align: top;  text-align: left">
                    <asp:TextBox ID="txt_LoginName" runat="server" Width="150px" MaxLength="200"></asp:TextBox><span style="color: #ff0000">*</span></td>
                <td style="vertical-align: top; text-align: left;width:10px">
                    <asp:RequiredFieldValidator ID="rfvLoginName" runat="server" ControlToValidate="txt_LoginName"
                        Display="None" ErrorMessage="Login Name required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revLoginName" runat="server" ControlToValidate="txt_LoginName"
                        Display="None" ErrorMessage="character  '  and  -   is not allowed for login name"
                        SetFocusOnError="True" ValidationExpression="([^'-]{1,200})"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td style=" height:1px">
                
                </td>
                <td style=" height:1px">
                
                </td>
                <td style=" height:1px">
                
                </td>
                <td style=" height:1px">
                
                </td>
                <td style=" height:1px">
                
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left;width:5px">
                </td>
                <td style="vertical-align: top; text-align: left">
                    <asp:Label ID="Label2" runat="server" Style="text-align: left" Text="Password"></asp:Label></td>
                <td style="vertical-align: top; text-align: left;width:5px">
                </td>
                <td style="vertical-align: top; text-align: left">
                    <asp:TextBox ID="txt_Password" runat="server" EnableViewState="False" TextMode="Password"
                        Width="150px" MaxLength="200"></asp:TextBox><span style="color: #ff0000">*</span></td>
                <td style="vertical-align: top; text-align: left;width:10px">
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txt_Password"
                        Display="None" ErrorMessage="Password required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txt_Password"
                        Display="None" ErrorMessage="character  '  is not allowed for password" SetFocusOnError="True"
                        ValidationExpression="[^']{1,200}"></asp:RegularExpressionValidator></td>
            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 5px; text-align: left;height: 1px">
                                </td>
                                <td style="vertical-align: top; text-align: left;height: 1px">
                                </td>
                                <td style="vertical-align: top; width: 5px; text-align: left;height: 1px">
                                </td>
                                <td style="vertical-align: top; text-align: left;height: 1px">
                                </td>
                                <td style="vertical-align: top; width: 10px; text-align: left;height: 1px">
                                </td>
                            </tr>

            <tr>
                <td style="vertical-align: top; text-align: left;width:5px">
                </td>
                <td style="vertical-align: top;  text-align: left">
                </td>
                <td style="vertical-align: top; text-align: left;width:5px">
                </td>
                <td style="vertical-align: top;  text-align: left">
                    <asp:Button ID="btn_Login" runat="server" Text="Login" OnClick="btn_Login_Click"/></td>
                <td style="vertical-align: top; text-align: left;width:10px">
                </td>
            </tr>
            <tr>
                <td style="height:10px">
                
                </td>
                <td style="height:10px">
                
                </td>
                <td style="height:10px">
                
                </td>
                <td style="height:10px">
                
                </td>
                <td style="height:10px">
                
                </td>
            </tr>
        </table>
                    </td>
                </tr>
            </table>
			        </td>
			    </tr>
			    
			</table>
			
			
			  </form>
			</div>
			</center> 
            
            </td>
        </tr>
   </table>
		
		</div>

</body>
</html>
