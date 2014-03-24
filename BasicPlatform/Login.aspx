<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BasicPlatform.Login" %>

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
        body {
            padding-top: 40px;
            padding-bottom: 40px;
            background-color: #f5f5f5;
        }

        .form-signin {
            max-width: 300px;
            padding: 19px 29px 29px;
            margin: 0 auto 20px;
            background-color: #fff;
            border: 1px solid #e5e5e5;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            -webkit-box-shadow: 0 1px 2px rgba(0,0,0,.05);
            -moz-box-shadow: 0 1px 2px rgba(0,0,0,.05);
            box-shadow: 0 1px 2px rgba(0,0,0,.05);
        }

            .form-signin .form-signin-heading, .form-signin .checkbox {
                margin-bottom: 10px;
            }

            .form-signin input[type="text"], .form-signin input[type="password"] {
                font-size: 16px;
                height: auto;
                margin-bottom: 15px;
                padding: 7px 9px;
            }
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
        <form class="form-signin" runat="server" id="signin" data-keyboard="on">
            <h2 class="form-signin-heading">Please sign in</h2>
            <p class="alert alert-error hide">
                Wrong password
            </p>
            <asp:TextBox runat="server" ID="username" CssClass="input-block-level" placeholder="Username / Email address" AutoCompleteType="None" />
            <asp:TextBox runat="server" ID="password" CssClass="input-block-level" placeholder="Password"
                TextMode="Password" />
            <label class="checkbox">
                <asp:CheckBox ID="rememberme" runat="server" />
                Remember me
            </label>
            <uc:Button runat="server" class="btn btn-large btn-primary" OnServerClick="SignIn">
                Sign In
            </uc:Button>
        </form>
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
    <script src="/assets/js/security.js"></script>
    <script type="text/javascript">
        $(function () {
            var onOk = function (f, data) {
                var apps = data.data.apps
                if (apps && apps.length) {
                    var i = 0
                    $(window).on('js.signin.signout', function () {
                        if (i < apps.length) {
                            $.getScript(apps[i] + '?logout=js.signin.signout')
                            i++
                        }
                        else location.replace(data.url)
                    }).trigger('js.signin.signout')
                }
            }
            var signin= new form('signin')
        })
    </script>
</body>
</html>
