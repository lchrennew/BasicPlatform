<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppNotFound.aspx.cs" Inherits="BasicPlatform.Auth.AppNotFound" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>Ops Gateway Platform</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
    <!-- Le styles -->
    <link href="/assets/css/bootstrap.css" rel="stylesheet">
    <style type="text/css">
        body { padding-top: 40px; padding-bottom: 40px; background-color: #f5f5f5; }
        
        .form-signin { max-width: 300px; padding: 19px 29px 29px; margin: 0 auto 20px; background-color: #fff; border: 1px solid #e5e5e5; -webkit-border-radius: 5px; -moz-border-radius: 5px; border-radius: 5px; -webkit-box-shadow: 0 1px 2px rgba(0,0,0,.05); -moz-box-shadow: 0 1px 2px rgba(0,0,0,.05); box-shadow: 0 1px 2px rgba(0,0,0,.05); }
        .form-signin .form-signin-heading, .form-signin .checkbox { margin-bottom: 10px; }
        .form-signin input[type="text"], .form-signin input[type="password"] { font-size: 16px; height: auto; margin-bottom: 15px; padding: 7px 9px; }
    </style>
    <link href="/assets/css/bootstrap-responsive.css" rel="stylesheet">
    <link href="/assets/css/select2.css" rel="stylesheet">
    <link href="/assets/css/common.admin.css" rel="stylesheet">
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="/assets/js/html5shiv.js"></script>
    <![endif]-->
    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="/assets/ico/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="/assets/ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="/assets/ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="/assets/ico/apple-touch-icon-57-precomposed.png">
    <link rel="shortcut icon" href="/assets/ico/favicon.png">
</head>
<body>
    <div class="container">
        <div class="page-header text-error">
            <h1>
                404 App Not Found</h1>
        </div>
        <div class="row">
            <div class="span12 text-center">
                <p class="alert alert-error">
                    <span class="label label-important">NOTE:</span>
                    The app you want to access can not be found or not available right now, please return back to your workspace home page.
                </p>
                <asp:LoginView runat="server">
                    <LoggedInTemplate>
                        <a href="/" class="btn btn-large btn-primary"><i class="icon-circle-arrow-left icon-white"></i> Return back to my dashboard</a>
                    </LoggedInTemplate>
                </asp:LoginView>
            </div>
        </div>
    </div>
    <!-- /container -->
    <!-- Le javascript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <!--[if lte IE 9]>
    <script src="/assets/js/jquery-1.9.1.js"></script>
    <![endif]-->
    <!--[if gt IE 9]><!-->
    <script src="/assets/js/jquery.js"></script>
    <!--<![endif]-->
    <script src="/assets/js/bootstrap.js"></script>
    <script src="/assets/js/select2.js"></script>
    <script src="/assets/js/ajax.js"></script>
    <script src="/assets/js/form.js"></script>
    <script src="/assets/js/common.js"></script>
</body>
</html>
