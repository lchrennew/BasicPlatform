<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="BasicPlatform.Logout" %>

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
        <div class="page-header text-error">
            <h1>Signing Out...</h1>
        </div>
        <div class="row">
            <div class="span12 text-center">
                <p class="alert alert-block">
                    <span class="label label-warning">NOTE:</span> Please wait for a moment while signing
                    out for all apps.
                </p>
                <div class="progress progress-striped active">
                    <div class="bar" style="width: 0%;">
                    </div>
                </div>
                <ul class="hide" id="apps">
                    <asp:Repeater runat="server" ID="rpt">
                        <ItemTemplate>
                            <li><%#Eval("Url") %></li>
                            <%--不要在li标签中增加任何其他字符，包括回车和空格，都会导致运行错误--%>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
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
    <script type="text/javascript">
        $(function () {
            var p = $('.progress .bar'), d = $('#apps li'), t = d.length, i = 0
            function getProgress() {
                p.width(parseInt(i / t * 100).toString() + '%')
            }

            if (t) {
                $(window).on('js.signout.signout', function () {
                    if (i < t) {
                        $.getScript($(d[i]).text() + '?logout=js.signout.signout')
                        i++
                        p.width(Math.round(i * 100 / t) + '%')
                    }
                    else setTimeout(function () { location.replace(location.search.deparam().returnurl || './') }, 1000)
                }).trigger('js.signout.signout')
            }
        });
    </script>
</body>
</html>
