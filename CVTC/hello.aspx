<%@ Page Language="C#" AutoEventWireup="true"  ValidateRequest="false" CodeFile="hello.aspx.cs" Inherits="hello" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html>

<head>

    <title>Untitled</title>

    <script language="javascript" type="text/javascript" src="js/jquery-1.4.2.js"></script>

   

  </head>

  <body>

      <p style="text-align:right;width:500px;">

          <span style="font-weight:bold;">Search:</span> <input type="text" id="txtSearch" name="txtSearch" maxlength="50" />&nbsp; 

          <img id="imgSearch" src="/images/cancel.gif" alt="Cancel Search" title="Cancel Search" style="width:150px;width:14px;height:14px;" />

      </p>

      <table id="tblSearch" cellpadding="2" cellspacing="0" border="1" style="width:500px;">

          <tr style="font-weight:bold;">

             <td>First</td>

             <td>Last</td>

             <td>Address</td>

             <td>City</td>

             <td>State</td>

         </tr>

         <tr>

             <td>John</td>

             <td>Dough</td>

            <td>123 Main Street</td>

            <td>Orlando</td>

            <td>Florida</td>

        </tr>

        <tr>

            <td>Jane</td>

            <td>Dough</td>

            <td>4367 South Washington Avenue</td>

            <td>Bartow</td>

            <td>California</td>

        </tr>

        <tr>

            <td>Bart</td>

            <td>Thompson</td>

            <td>531 Townsend Circle</td>

            <td>Atlanta</td>

            <td>Georgia</td>

        </tr>

        <tr>

            <td>Sherry</td>

            <td>Simpson</td>

            <td>3346 Presario Lane, Apt. 123</td>

            <td>Seattle</td>

            <td>Washington</td>

        </tr>

        <tr>

            <td>Matt</td>

            <td>Damon</td>

            <td>300 Pounds Street</td>

            <td>Boston</td>

            <td>Massachusetts</td>

        </tr>

    </table>

    <script language="javascript" type="text/javascript">

        jQuery.expr[":"].containsNoCase = function(el, i, m) {

            var search = m[3];

            if (!search) return false;

            return eval("/" + search + "/i").test($(el).text());

        };

 

        jQuery(document).ready(function() {

            // used for the first example in the blog post

            jQuery('li:contains(\'DotNetNuke\')').css('color', '#0000ff').css('font-weight', 'bold');

 

            // hide the cancel search image

            jQuery('#imgSearch').hide();

 

            // reset the search when the cancel image is clicked

            jQuery('#imgSearch').click(function() {

                resetSearch();

            });

 

            // cancel the search if the user presses the ESC key

            jQuery('#txtSearch').keyup(function(event) {

                if (event.keyCode == 27) {

                    resetSearch();

          }

            });

 

            // execute the search

            jQuery('#txtSearch').keyup(function() {

                // only search when there are 3 or more characters in the textbox

                if (jQuery('#txtSearch').val().length > 2) {

                    // hide all rows

                    jQuery('#tblSearch tr').hide();

                    // show the header row

                    jQuery('#tblSearch tr:first').show();

                    // show the matching rows (using the containsNoCase from Rick Strahl)

                    jQuery('#tblSearch tr td:containsNoCase(\'' + jQuery('#txtSearch').val() + '\')').parent().show();

                    // show the cancel search image

                    jQuery('#imgSearch').show();

                }

                else if (jQuery('#txtSearch').val().length == 0) {

                    // if the user removed all of the text, reset the search

                    resetSearch();

                }

 

                // if there were no matching rows, tell the user

                if (jQuery('#tblSearch tr:visible').length == 1) {

                    // remove the norecords row if it already exists

                    jQuery('.norecords').remove();

                    // add the norecords row

                    jQuery('#tblSearch').append('<tr class="norecords"><td colspan="5" class="Normal">No records were found</td></tr>');

                }

            });

        });

 

        function resetSearch() {

            // clear the textbox

            jQuery('#txtSearch').val('');

            // show all table rows

            jQuery('#tblSearch tr').show();

            // remove any no records rows

            jQuery('.norecords').remove();

            // remove the cancel search image

            jQuery('#imgSearch').hide();

            // make sure we re-focus on the textbox for usability

            jQuery('#txtSearch').focus();

        }

    

</script>

</body>

</html>


