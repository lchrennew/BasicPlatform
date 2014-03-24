<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="BasicPlatform.ResetPassword" %>

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
        <form class="form-signin" runat="server" id="resetpwd" data-keyboard="on">
            <h2 class="form-signin-heading">Please use stronger password</h2>
            <p class="alert alert-error hide">
                Wrong password
            </p>
            <asp:TextBox runat="server" ID="username" CssClass="input-block-level" placeholder="Username / Email address" AutoCompleteType="None" />
            <asp:TextBox runat="server" ID="oldpwd" CssClass="input-block-level" placeholder="Old password"
                TextMode="Password" />
            <asp:TextBox runat="server" ID="newpwd" CssClass="input-block-level" placeholder="New password"
                TextMode="Password" />
            <uc:Button runat="server" class="btn btn-large btn-primary" OnServerClick="SignInAndResetPassword">
                Reset Password and Sign In
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
            var resetpwd = new form('resetpwd')
            resetpwd.cg('oldpwd', function (cg) {
                var v = cg.j.val()
                if (v.length && v.getPwdLv() < 60) return cg.ok()
                else return cg.err()
            })
            resetpwd.cg('newpwd', function (cg) {
                var v = cg.j.val()
                if (v.length && v.getPwdLv() >= 60) return cg.ok()
                else return cg.err()
            })
        })

        $(function () {
            var lvs = [
            { bar: 'progress progress-danger', text: 'Dangerous', tc: 'text-error' },
            { bar: 'progress progress-warning', text: 'Tolerable', tc: 'text-warning' },
            { bar: 'progress progress-success', text: 'Good', tc: 'text-success' },
            { bar: 'progress progress-success', text: 'Perfect', tc: 'text-success' }
            ]
            function getClass(s) {
                if (s < 60) return lvs[0]
                else if (s < 80) return lvs[1]
                else if (s < 90) return lvs[2]
                else return lvs[3]
            }
            $('#newpwd')
                .popover({
                    html: true,
                    placement: 'right',
                    content: function () {
                        var c = $('<div style="width:200px;"><p>Strength: <span id="pwdlv">0</span></p><p>Scrore: <span id="pwdscr">0</span></p><div class="progress"><div class="bar"></div></div></div>'),
                            s = $(this).val().getPwdLv(),
                            lv = getClass(s)
                        c.find('#pwdlv').html(lv.text).attr('class', lv.tc)
                        c.find('#pwdscr').html(s).attr('class', lv.tc)
                        c.find('.progress').addClass(lv.bar).find('.bar').css('width', s + '%')
                        return c
                    },
                    trigger: 'focus'
                })
            .on('keypress change keydown, keyup', function (event) {
                var s = $(this).val().getPwdLv(),
                            lv = getClass(s)
                $('#pwdlv').html(lv.text).attr('class', lv.tc).parent().next().find('#pwdscr').html(s).attr('class', lv.tc).parent().next().attr('class', lv.bar).find('.bar').css('width', s + '%')
            })
        })
    </script>
</body>
</html>
