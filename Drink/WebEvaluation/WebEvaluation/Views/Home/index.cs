@{
    ViewBag.Title = "Contact";
}

<div >
    <h3>機能一覧</h3>
</div
>
<div >
    <h4>店舗</h4>
    <ul >
        <li>@Html.ActionLink("パーティ一覧", "Index", "Party")</li>
        <li>@Html.ActionLink("レポート登録", "Index", "Party")</li>
    </ul>
</div>

<div>
    <h4>カスタマセンター</h4>
    <ul>
        <li>@Html.ActionLink("担当者評価", "Index", "Party")</li>
        <li>@Html.ActionLink("上長評価", "Index", "Party")</li>
        <li>@Html.ActionLink("社員管理", "Index", "Employee")</li>
        <li>@Html.ActionLink("店舗管理", "Index", "Shop")</li>
    </ul>
</div>

<div>
    <H4>共通機能</H4>
    <ul>
        <li>@Html.ActionLink("テレレポート", "Index", "TelRpt")</li>
        <li>@Html.ActionLink("通話率一覧", "Index", "TelRateRpt")</li>
        <li>@Html.ActionLink("評価統計", "Index", "ValRpt")</li>
    </ul>
</div>

<div>
    <H4>管理者機能</H4>
    <ul>
        <li>@Html.ActionLink("社員データインポート", "Index", "Employee")</li>
        <li>@Html.ActionLink("店舗テータインポート", "Index", "Shop")</li>
        <li>@Html.ActionLink("エリア一覧", "Index", "Shop")</li>
        <li>@Html.ActionLink("事業所一覧", "Index", "Shop")</li>
        <li>@Html.ActionLink("グループ一覧", "Index", "Shop")</li>
        <li>@Html.ActionLink("ユーザ管理", "Index", "Shop")</li>
    </ul>
</div>