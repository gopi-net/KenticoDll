<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_CMS_CheerCard_Pages_CheerCardRequest_Data_PrintItem"
    ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    EnableEventValidation="false" CodeFile="CheerCardRequest_Data_PrintItem.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <style>
@font-face {
  font-family: 'Core-icons';
  src: url('/App_Themes/Default/Fonts/Core-icons.eot');
  src: url('/App_Themes/Default/Fonts/Core-icons.svg#Core-icons') format('svg'), url('/App_Themes/Default/Fonts/Core-icons.eot?#iefix') format('embedded-opentype'), url('/App_Themes/Default/Fonts/Core-icons.ttf') format('truetype'), url('/App_Themes/Default/Fonts/Core-icons.woff') format('woff');
  font-weight: normal;
  font-style: normal;
}
.body.cms-bootstrap, .cms-bootstrap {
            font-family: "Segoe UI",Helvetica,Verdana,Arial,sans-serif;
            font-size: 14px;
        }

        .DialogsPageHeader {
            z-index: 1 !important;
        }

        .cms-bootstrap .dialog-header {
            background: none repeat scroll 0 0 #262524;
            padding-left: 16px;
        }

        .non-selectable {
            -moz-user-select: none;
        }

        .cms-bootstrap .dialog-header .dialog-header-action-buttons {
            display: table;
            float: right !important;
            text-align: center;
        }

            .cms-bootstrap .dialog-header .dialog-header-action-buttons .action-button {
                display: table-cell;
                height: 48px;
                vertical-align: middle;
            }

                .cms-bootstrap .dialog-header .dialog-header-action-buttons .action-button a {
                    color: #a3a2a2;
                    display: table-cell;
                    height: 32px;
                    min-width: 32px;
                    text-decoration: none;
                    vertical-align: middle;
                }

        .cms-bootstrap a {
            background: none repeat scroll 0 0 transparent;
        }

        .cms-bootstrap a, .cms-bootstrap .link {
            color: #0f6194;
            cursor: pointer;
            text-decoration: underline;
        }

        .cms-bootstrap a {
            background: none repeat scroll 0 0 transparent;
        }

        .cms-bootstrap .sr-only {
            border: 0 none;
            clip: rect(0px, 0px, 0px, 0px);
            height: 1px;
            margin: -1px;
            overflow: hidden;
            padding: 0;
            position: absolute;
            width: 1px;
        }

        .cms-bootstrap .icon-modal-maximize:before {
            content: "\e649";
        }

        

        .cms-bootstrap .cms-icon-80 {
            font-size: 16px;
            height: 16px;
            padding: 4px;
            width: 16px;
        }

        .cms-bootstrap .dialog-header .dialog-header-action-buttons .close-button {
            background-color: #403e3d;
        }

        element.style {
            cursor: pointer;
        }

        .cms-bootstrap .icon-modal-close:before {
            content: "\e64a";
            font-size: 32px;
            height: 32px;
            width: 32px;
            padding: 0px ;
        }


        .cms-bootstrap [class^="icon-"], .cms-bootstrap [class*=" icon-"] {
            display: inline-block;
            font-family: 'Core-icons';
            font-size: 16px;
            font-style: normal;
            font-variant: normal;
            font-weight: normal;
            line-height: 1;
            text-transform: none;
        }

        .cms-bootstrap .dialog-header .dialog-header-title {
            color: #fff;
            display: inline;
            font-size: 24px;
            position: relative;
        }

        .cms-bootstrap .dialog-header h2 {
            font-weight: bold;
            line-height: 48px;
        }

        .cms-bootstrap h2, .cms-bootstrap .h2 {
            color: #0f6194;
            font-size: 32px;
            font-weight: bold;
            line-height: 40px;
            margin-bottom: 24px;
        }

        .cms-bootstrap h1, .cms-bootstrap h2, .cms-bootstrap h3, .cms-bootstrap h4, .cms-bootstrap h5, .cms-bootstrap h6 {
            margin-top: 0;
        }

        .cms-bootstrap h1, .cms-bootstrap h2, .cms-bootstrap h3, .cms-bootstrap h4, .cms-bootstrap h5, .cms-bootstrap h6, .cms-bootstrap .h1, .cms-bootstrap .h2, .cms-bootstrap .h3, .cms-bootstrap .h4, .cms-bootstrap .h5, .cms-bootstrap .h6 {
            font-family: "Segoe UI",Helvetica,Verdana,Arial,sans-serif;
            font-weight: bold;
            line-height: 20px;
        }

        .cms-bootstrap .dialog-footer {
            background: none repeat scroll 0 0 #d6d9d6;
            box-sizing: border-box;
            display: block;
            height: 64px;
            padding: 16px;
            text-align: right;
            z-index: 11050;
        }

        .cms-bootstrap .control-group-inline {
            word-spacing: -4px;
        }

            .cms-bootstrap .control-group-inline .btn, .cms-bootstrap .control-group-inline .btn-group, .cms-bootstrap .control-group-inline .btn-dropdown, .cms-bootstrap .control-group-inline div:not(.control-group-inline):not(.keep-white-space-fixed), .cms-bootstrap .control-group-inline span, .cms-bootstrap .control-group-inline .form-control, .cms-bootstrap .control-group-inline label, .cms-bootstrap .control-group-inline a, .cms-bootstrap .control-group-inline p {
                word-spacing: 0;
            }

        .cms-bootstrap .dialog-footer .FloatLeft, .cms-bootstrap .dialog-footer .FloatRight {
            word-spacing: 0;
        }

        .FloatRight, .RTL .FloatLeft {
            float: right;
            text-align: right;
        }

        .FloatRight, .RTL .FloatLeft {
            float: right;
            text-align: right;
        }

        .cms-bootstrap .dialog-footer .FloatLeft .btn, .cms-bootstrap .dialog-footer .FloatRight .btn, .cms-bootstrap .dialog-footer .FloatLeft .btn-group, .cms-bootstrap .dialog-footer .FloatRight .btn-group, .cms-bootstrap .dialog-footer .FloatLeft .btn-dropdown, .cms-bootstrap .dialog-footer .FloatRight .btn-dropdown, .cms-bootstrap .dialog-footer .FloatLeft div:not(.control-group-inline):not(.keep-white-space-fixed), .cms-bootstrap .dialog-footer .FloatRight div:not(.control-group-inline):not(.keep-white-space-fixed), .cms-bootstrap .dialog-footer .FloatLeft span, .cms-bootstrap .dialog-footer .FloatRight span, .cms-bootstrap .dialog-footer .FloatLeft .form-control, .cms-bootstrap .dialog-footer .FloatRight .form-control, .cms-bootstrap .dialog-footer .FloatLeft label, .cms-bootstrap .dialog-footer .FloatRight label, .cms-bootstrap .dialog-footer .FloatLeft a, .cms-bootstrap .dialog-footer .FloatRight a, .cms-bootstrap .dialog-footer .FloatLeft p, .cms-bootstrap .dialog-footer .FloatRight p {
            word-spacing: 0;
        }

        .cms-bootstrap .control-group-inline .btn, .cms-bootstrap .control-group-inline .btn-dropdown {
            vertical-align: top;
        }

        .cms-bootstrap .control-group-inline .btn, .cms-bootstrap .control-group-inline .btn-group, .cms-bootstrap .control-group-inline .btn-dropdown, .cms-bootstrap .control-group-inline div:not(.control-group-inline):not(.keep-white-space-fixed), .cms-bootstrap .control-group-inline span, .cms-bootstrap .control-group-inline .form-control, .cms-bootstrap .control-group-inline label, .cms-bootstrap .control-group-inline a, .cms-bootstrap .control-group-inline p {
            word-spacing: 0;
        }

        .cms-bootstrap .btn-primary {
            background-color: #497d04;
            box-shadow: 0 -3px 0 #355e00 inset;
            color: #fff;
            margin: 0;
        }

        .cms-bootstrap .btn {
            -moz-user-select: none;
            border: medium none;
            border-radius: 3px;
            cursor: pointer;
            display: inline-block;
            font-family: "Segoe UI Semibold",Helvetica,Verdana,Arial,sans-serif;
            font-size: 14px;
            font-weight: 600;
            height: 32px;
            line-height: 32px;
            margin: 0;
            padding: 0 16px;
            text-align: center;
            text-decoration: none;
            vertical-align: middle;
            white-space: nowrap;
            width: auto;
        }

        .cms-bootstrap button, .cms-bootstrap html input[type="button"], .cms-bootstrap input[type="reset"], .cms-bootstrap input[type="submit"] {
            cursor: pointer;
        }

        .cms-bootstrap button, .cms-bootstrap select {
            text-transform: none;
        }

        .cms-bootstrap button {
            overflow: visible;
        }

        .cms-bootstrap button, .cms-bootstrap input, .cms-bootstrap optgroup, .cms-bootstrap select, .cms-bootstrap textarea {
            font-family: inherit;
            font-size: 100%;
            margin: 0;
        }

            .cms-bootstrap button, .cms-bootstrap input, .cms-bootstrap select[multiple], .cms-bootstrap textarea {
                background-image: none;
            }

                .cms-bootstrap button, .cms-bootstrap html input[type="button"], .cms-bootstrap input[type="reset"], .cms-bootstrap input[type="submit"] {
                    cursor: pointer;
                }

        .cms-bootstrap button, .cms-bootstrap select {
            text-transform: none;
        }

        .cms-bootstrap button {
            overflow: visible;
        }

        .cms-bootstrap button, .cms-bootstrap input, .cms-bootstrap optgroup, .cms-bootstrap select, .cms-bootstrap textarea {
            font-family: inherit;
            font-size: 100%;
            margin: 0;
        }

        [class^="icon-"], [class*=" icon-"] {
            background-image: url('') !important;
            height: 32px!important;
            width: 32px!important;
        }
    </style>

    <script type="text/javascript">
        function getPrint() {
            window.open('../../CMS_EmergeCommon/PrintPage.aspx', 'Print', 'toolbar=no,location=no,menubar=no,scrollbars=yes,resizable=no,width=450,height=300,top=150,left=300');
        }

    </script>

    <div class="box-page-content ">
        <div class="PageContent" id="divPrint">
            <div class=" box-cheerCard">
                <div class="box-cheerCard-Form">
                    <section class="previewCard">
                        <div>
                            <cms:LocalizedLiteral ID="contentToPrint" runat="server"></cms:LocalizedLiteral>
                        </div>
                    </section>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnPrint" runat="server" CssClass="SubmitButton" ResourceString="Emerge.CC.Caption.Print"
            OnClientClick="getPrint();return false;" EnableViewState="false" />

        <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" ResourceString="general.close"
            OnClientClick="return CloseDialog();" EnableViewState="false" />
    </div>
</asp:Content>
