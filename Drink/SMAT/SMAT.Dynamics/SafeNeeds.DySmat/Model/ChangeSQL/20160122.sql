
alter table Y_Entity add   EntityGroup nvarchar(20) ;
alter table Y_Entity add   EntityState nvarchar(10) ;

alter table Y_EntityField add   DefaultValue nvarchar(40) ;

alter table Y_EntityForm add   Belong nvarchar(40) ;

alter table Y_EntityUserControl add   Belong nvarchar(40) ;



alter table Y_Entity add   CreatedBy nvarchar(2) ;

alter table Y_EntityForm add   FormCategory nvarchar(20) ;
alter table Y_EntityForm add   CreatedBy nvarchar(2) ;
alter table Y_EntityForm add   FormState nvarchar(10) ;

alter table Y_EntityUserControl add   UserControlCategory nvarchar(20) ;
alter table Y_EntityUserControl add   CreatedBy nvarchar(2) ;
alter table Y_EntityUserControl add   UserControlState nvarchar(10) ;

alter table Y_EntityFilter add   Belong nvarchar(40) ;
alter table Y_EntityFilter add   FilterCategory nvarchar(20) ;
alter table Y_EntityFilter add   CreatedBy nvarchar(2) ;
alter table Y_EntityFilter add   FilterState nvarchar(10) ;

alter table Y_EntityFilterControl add   Belong nvarchar(40) ;
alter table Y_EntityFilterControl add   FilterCategory nvarchar(20) ;
alter table Y_EntityFilterControl add   CreatedBy nvarchar(2) ;
alter table Y_EntityFilterControl add   FilterState nvarchar(10) ;

alter table Y_EntityView add   Belong nvarchar(40) ;
alter table Y_EntityView add   ViewCategory nvarchar(20) ;
alter table Y_EntityView add   CreatedBy nvarchar(2) ;
alter table Y_EntityView add   ViewState nvarchar(10) ;


update Y_EntityForm set FormDesc =FormName where FormDesc is null;

update Y_EntityUserControl set UserControlDesc =UserControlName where UserControlDesc is null;


--=======Y_Entity=====================================
--droup constraint key
alter table Y_Entity drop constraint [PK_dbo.Y_Entity];

alter table Y_Entity alter column EntityName nvarchar(40) not null;

alter table Y_Entity alter column EntityDesc nvarchar(40);
--add constraint key
alter table Y_Entity add constraint [PK_dbo.Y_Entity] primary key 
(
	[ProjID] ASC,
	[EntityName] ASC
);
--=======Y_Entity=====================================

--=======Y_EntityField=====================================
--droup constraint key
alter table Y_EntityField drop constraint [PK_dbo.Y_EntityField];

alter table Y_EntityField alter column EntityName nvarchar(40) not null;

alter table Y_EntityField alter column FieldName nvarchar(40) not null;
--add constraint key
alter table Y_EntityField add constraint [PK_dbo.Y_EntityField] primary key 
(
	[ProjID] ASC,
	[EntityName] ASC,
	[FieldName] ASC
);
--=======Y_EntityField=====================================


--=======Y_EntityForm=====================================
--droup constraint key
alter table Y_EntityForm drop constraint [PK_dbo.Y_EntityForm];

alter table Y_EntityForm alter column EntityName nvarchar(40) not null;

alter table Y_EntityForm alter column FormName nvarchar(40) not null;

alter table Y_EntityForm alter column FormDesc nvarchar(40);

--add constraint key
alter table Y_EntityForm add constraint [PK_dbo.Y_EntityForm] primary key 
(
	[ProjID] ASC,
	[EntityName] ASC,
	[FormName] ASC
);
--=======Y_EntityForm=====================================



--=======Y_EntityFormControl=====================================
--droup constraint key
alter table Y_EntityFormControl drop constraint [PK_dbo.Y_EntityFormControl];

alter table Y_EntityFormControl alter column EntityName nvarchar(40) not null;

alter table Y_EntityFormControl alter column FormName nvarchar(40) not null;

alter table Y_EntityFormControl alter column ControlName nvarchar(40) not null;
--add constraint key
alter table Y_EntityFormControl add constraint [PK_dbo.Y_EntityFormControl] primary key 
(
	[ProjID] ASC,
	[EntityName] ASC,
	[FormName] ASC,
	[ControlName] ASC
);
--=======Y_EntityFormControl=====================================

--=======Y_EntityUserControl=====================================
--droup constraint key
alter table Y_EntityUserControl drop constraint [PK_dbo.Y_EntityUserControl];

alter table Y_EntityUserControl alter column EntityName nvarchar(40) not null;

alter table Y_EntityUserControl alter column UserControlName nvarchar(40) not null;

alter table Y_EntityUserControl alter column UserControlDesc nvarchar(40) ;
--add constraint key
alter table Y_EntityUserControl add constraint [PK_dbo.Y_EntityUserControl] primary key 
(
	[ProjID] ASC,
	[EntityName] ASC,
	[UserControlName] ASC
);
--=======Y_EntityUserControl=====================================

--=======Y_EntityUserControlItem=====================================
--droup constraint key
alter table Y_EntityUserControlItem drop constraint [PK_dbo.Y_EntityUserControlItem];

alter table Y_EntityUserControlItem alter column EntityName nvarchar(40) not null;

alter table Y_EntityUserControlItem alter column UserControlName nvarchar(40) not null;

alter table Y_EntityUserControlItem alter column ControlName nvarchar(40) not null ;
--add constraint key
alter table Y_EntityUserControlItem add constraint [PK_dbo.Y_EntityUserControlItem] primary key 
(
	[ProjID] ASC,
	[EntityName] ASC,
	[UserControlName] ASC,
	[ControlName] ASC
);
--=======Y_EntityUserControlItem=====================================

--=======Y_EntityUserControlItem=====================================
--droup constraint key
alter table Y_EntityUserControlItem drop constraint [PK_dbo.Y_EntityUserControlItem];

alter table Y_EntityUserControlItem alter column EntityName nvarchar(40) not null;

alter table Y_EntityUserControlItem alter column UserControlName nvarchar(40) not null;

alter table Y_EntityUserControlItem alter column ControlName nvarchar(40) not null ;
--add constraint key
alter table Y_EntityUserControlItem add constraint [PK_dbo.Y_EntityUserControlItem] primary key 
(
	[ProjID] ASC,
	[EntityName] ASC,
	[UserControlName] ASC,
	[ControlName] ASC
);
--=======Y_EntityUserControlItem=====================================

--=======Y_EntityRela1N=====================================
--droup constraint key
alter table Y_EntityRela1N drop constraint [PK_dbo.Y_EntityRela1N];

alter table Y_EntityRela1N alter column EntityName nvarchar(40) not null;

alter table Y_EntityRela1N alter column RelaName nvarchar(80) not null;

alter table Y_EntityRela1N alter column RelaDesc nvarchar(80);

alter table Y_EntityRela1N alter column RelaEntityName nvarchar(40);
alter table Y_EntityRela1N alter column FieldNames nvarchar(80);
alter table Y_EntityRela1N alter column RelaFieldNames nvarchar(80);

--add constraint key
alter table Y_EntityRela1N add constraint [PK_dbo.Y_EntityRela1N] primary key 
(
	[ProjID] ASC,
	[EntityName] ASC,
	[RelaName] ASC
);
--=======Y_EntityRela1N=====================================

--=======Y_EntityRelaN1=====================================
--droup constraint key
alter table Y_EntityRelaN1 drop constraint [PK_dbo.Y_EntityRelaN1];

alter table Y_EntityRelaN1 alter column EntityName nvarchar(40) not null;

alter table Y_EntityRelaN1 alter column RelaName nvarchar(80) not null;

alter table Y_EntityRelaN1 alter column RelaDesc nvarchar(80);

alter table Y_EntityRelaN1 alter column RelaEntityName nvarchar(40);
alter table Y_EntityRelaN1 alter column FieldNames nvarchar(80);
alter table Y_EntityRelaN1 alter column RelaIFieldNames nvarchar(80);

--add constraint key
alter table Y_EntityRelaN1 add constraint [PK_dbo.Y_EntityRelaN1] primary key 
(
	[ProjID] ASC,
	[EntityName] ASC,
	[RelaName] ASC
);
--=======Y_EntityRelaN1=====================================



--=======Y_EntityFilter=====================================
--droup constraint key
alter table Y_EntityFilter drop constraint [PK_dbo.Y_EntityFilter];

alter table Y_EntityFilter alter column EntityName nvarchar(40) not null;

alter table Y_EntityFilter alter column FilterName nvarchar(80) not null;

alter table Y_EntityFilter alter column ItemEntityAliasName nvarchar(80);

--add constraint key
alter table Y_EntityFilter add constraint [PK_dbo.Y_EntityFilter] primary key 
(
	[ProjID] ASC,
	[EntityName] ASC,
	[FilterName] ASC
);
--=======Y_EntityFilter=====================================



--=======Y_EntityFilterControl=====================================
--droup constraint key
alter table Y_EntityFilterControl drop constraint [PK_dbo.Y_EntityFilterControl];

alter table Y_EntityFilterControl alter column EntityName nvarchar(40) not null;

alter table Y_EntityFilterControl alter column FilterControlName nvarchar(40) not null;

alter table Y_EntityFilterControl alter column FilterControlDesc nvarchar(80);
alter table Y_EntityFilterControl alter column FilterNames nvarchar(120);
alter table Y_EntityFilterControl alter column UserControlName nvarchar(40);

--add constraint key
alter table Y_EntityFilterControl add constraint [PK_dbo.Y_EntityFilterControl] primary key 
(
	[ProjID] ASC,
	[EntityName] ASC,
	[FilterControlName] ASC
);
--=======Y_EntityFilterControl=====================================

--=======Y_EntityView=====================================
--droup constraint key
alter table Y_EntityView drop constraint [PK_dbo.Y_EntityView];

alter table Y_EntityView alter column EntityName nvarchar(40) not null;

alter table Y_EntityView alter column ViewName nvarchar(40) not null;

--add constraint key
alter table Y_EntityView add constraint [PK_dbo.Y_EntityView] primary key 
(
	[ProjID] ASC,
	[EntityName] ASC,
	[ViewName] ASC
);
--=======Y_EntityView=====================================

--=======Y_EntityViewItem=====================================
--droup constraint key
alter table Y_EntityViewItem drop constraint [PK_dbo.Y_EntityViewItem];

alter table Y_EntityViewItem alter column EntityName nvarchar(40) not null;

alter table Y_EntityViewItem alter column ViewName nvarchar(40) not null;

alter table Y_EntityViewItem alter column ItemName nvarchar(40) not null;

--add constraint key
alter table Y_EntityViewItem add constraint [PK_dbo.Y_EntityViewItem] primary key 
(
	[ProjID] ASC,
	[EntityName] ASC,
	[ViewName] ASC,
	[ItemName] ASC
);
--=======Y_EntityViewItem=====================================


--20160224
--=======Y_EntityField=====================================
alter table Y_EntityField add FormX int default 0;
alter table Y_EntityField add FormY int default 0;
alter table Y_EntityField add SystemField int default 0;
--=======Y_EntityField=====================================


update Y_EntityField set FormX = 0 ,FormY = 0,SystemField = 0;


--20160226
alter table Y_EntityViewItem add   ItemCategory nvarchar(20) ;
alter table Y_EntityViewItem add   ItemFieldName nvarchar(40) ;

alter table Y_EntityField add   FieldCategory nvarchar(20) ;

alter table Y_EntityFilter add   IsHaving bit default 0 ;

update Y_EntityFilter set IsHaving = 0 ;



alter table Y_EntityViewItem add   IsHideInView bit default 0 ;
alter table Y_EntityViewItem add   SumType nvarchar(2) ;

update Y_EntityViewItem set IsHideInView = 0 ;

drop table [Y_EntityViewFilter];
create table [Y_EntityViewFilter] (
  ProjID int not null
  , EntityName nvarchar(40) not null
  , ViewName nvarchar(40) not null
  , FilterControlName nvarchar(40) not null
  , Seq smallint 
  , primary key (ProjID,EntityName,ViewName,FilterControlName)
);


--=================
alter table Y_EntityForm add  FormNo nvarchar(10) ;

--================

alter table Y_MenuGroup add  ParentGroupName nvarchar(20) ;

alter table Y_RoleMenu add  MenuAuthType nvarchar(10) ;

create table [Y_RoleMenuAuth] (
  ProjID int not null
  , RoleName nvarchar(40) not null
  , MenuName nvarchar(40) not null
  , AuthName nvarchar(80) not null
  , AuthType nvarchar(10) 
  , EntityName nvarchar(40)
  , FormName nvarchar(40)
  , FieldName nvarchar(40)
  , AuthValue nvarchar(1)
  , primary key (ProjID,RoleName,MenuName,AuthName)
);


--=================
alter table Y_EntityForm add  GroupName nvarchar(40) ;
alter table Y_EntityForm add  GroupSeq nvarchar(6) ;
alter table Y_EntityForm add  GroupLinkDesc nvarchar(40) ;

alter table Y_EntityForm add  CreatedUserName nvarchar(10) ;



alter table Y_MenuGroupDetail add  DetailDesc nvarchar(100) ;


--==========================================================
--droup constraint key
alter table Y_EntityViewItem drop constraint [PK_dbo.Y_EntityViewItem];

alter table Y_EntityViewItem alter column ItemName nvarchar(80) not null;
alter table Y_EntityViewItem alter column ItemDesc nvarchar(80);

--add constraint key
alter table Y_EntityViewItem add constraint [PK_dbo.Y_EntityViewItem] primary key 
(
	[ProjID] ASC,
	[EntityName] ASC,
	[ViewName] ASC,
	[ItemName] ASC
);



alter table Y_EntityFilter alter column FilterDesc nvarchar(80);


alter table Y_EntityField add   IsEncryption bit default 0 ;
alter table Y_EntityField add   EncryptionType nvarchar(20) ;
update Y_EntityField set IsEncryption = 0 ;

update Y_EntityUserControl set UserControlCategory = 'SearchCondition' ;

