PGDMP  :                    |            ShopProjectDB    16.1    16.0 �    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    16397    ShopProjectDB    DATABASE     �   CREATE DATABASE "ShopProjectDB" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE "ShopProjectDB";
                postgres    false            �            1259    24663    Order_consignments    TABLE     �   CREATE TABLE public."Order_consignments" (
    "Order_consignment_id" integer NOT NULL,
    "Worker_id" integer NOT NULL,
    "Date_time" timestamp with time zone NOT NULL
);
 (   DROP TABLE public."Order_consignments";
       public         heap    postgres    false            �            1259    24662 #   Accepting_orders_Consignment_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Accepting_orders_Consignment_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 <   DROP SEQUENCE public."Accepting_orders_Consignment_id_seq";
       public          postgres    false    241            �           0    0 #   Accepting_orders_Consignment_id_seq    SEQUENCE OWNED BY     y   ALTER SEQUENCE public."Accepting_orders_Consignment_id_seq" OWNED BY public."Order_consignments"."Order_consignment_id";
          public          postgres    false    240            �            1259    16408 
   Categories    TABLE     l   CREATE TABLE public."Categories" (
    "Category_id" integer NOT NULL,
    "Category_name" text NOT NULL
);
     DROP TABLE public."Categories";
       public         heap    postgres    false            �            1259    16407    Categories_Category_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Categories_Category_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 3   DROP SEQUENCE public."Categories_Category_id_seq";
       public          postgres    false    218            �           0    0    Categories_Category_id_seq    SEQUENCE OWNED BY     _   ALTER SEQUENCE public."Categories_Category_id_seq" OWNED BY public."Categories"."Category_id";
          public          postgres    false    217            �            1259    16494    Payments    TABLE       CREATE TABLE public."Payments" (
    "Payment_id" integer NOT NULL,
    "Shop_id" integer NOT NULL,
    "Approvier_worker_id" integer,
    "Recipient_worker_id" integer NOT NULL,
    "Amount" money NOT NULL,
    "Is_approved" boolean DEFAULT false NOT NULL,
    "Comment" text
);
    DROP TABLE public."Payments";
       public         heap    postgres    false            �            1259    16493    Payments_Payment_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Payments_Payment_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE public."Payments_Payment_id_seq";
       public          postgres    false    231            �           0    0    Payments_Payment_id_seq    SEQUENCE OWNED BY     Y   ALTER SEQUENCE public."Payments_Payment_id_seq" OWNED BY public."Payments"."Payment_id";
          public          postgres    false    230            �            1259    16485 	   Positions    TABLE     �   CREATE TABLE public."Positions" (
    "Position_id" integer NOT NULL,
    "Position_name" text NOT NULL,
    "Salary_type_id" integer NOT NULL,
    "Role_id" integer
);
    DROP TABLE public."Positions";
       public         heap    postgres    false            �            1259    16484    Positions_Position_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Positions_Position_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 2   DROP SEQUENCE public."Positions_Position_id_seq";
       public          postgres    false    229            �           0    0    Positions_Position_id_seq    SEQUENCE OWNED BY     ]   ALTER SEQUENCE public."Positions_Position_id_seq" OWNED BY public."Positions"."Position_id";
          public          postgres    false    228            �            1259    16468    Product_consignment_product    TABLE     �   CREATE TABLE public."Product_consignment_product" (
    "Product_id" integer NOT NULL,
    "Product_consignment_id" integer NOT NULL,
    "Count" integer NOT NULL
);
 1   DROP TABLE public."Product_consignment_product";
       public         heap    postgres    false            �            1259    16457    Product_consignments    TABLE     �   CREATE TABLE public."Product_consignments" (
    "Product_consignment_id" integer NOT NULL,
    "Worker_id" integer NOT NULL,
    "Date_time" timestamp with time zone NOT NULL,
    "Order_consignment_id" integer NOT NULL
);
 *   DROP TABLE public."Product_consignments";
       public         heap    postgres    false            �            1259    16456 /   Product_consignments_Product_consignment_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Product_consignments_Product_consignment_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 H   DROP SEQUENCE public."Product_consignments_Product_consignment_id_seq";
       public          postgres    false    226            �           0    0 /   Product_consignments_Product_consignment_id_seq    SEQUENCE OWNED BY     �   ALTER SEQUENCE public."Product_consignments_Product_consignment_id_seq" OWNED BY public."Product_consignments"."Product_consignment_id";
          public          postgres    false    225            �            1259    24691     Product_order_product_in_storage    TABLE     �   CREATE TABLE public."Product_order_product_in_storage" (
    "Product_order_id" integer NOT NULL,
    "Product_in_storage_id" integer NOT NULL,
    "Product_count" integer NOT NULL
);
 6   DROP TABLE public."Product_order_product_in_storage";
       public         heap    postgres    false            �            1259    24675    Product_orders    TABLE     �   CREATE TABLE public."Product_orders" (
    "Product_order_id" integer NOT NULL,
    "Date_time" timestamp with time zone NOT NULL,
    "Worker_id" integer NOT NULL,
    "Order_consignment_id" integer NOT NULL,
    "Is_approved" boolean
);
 $   DROP TABLE public."Product_orders";
       public         heap    postgres    false            �            1259    24674 #   Product_orders_Product_order_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Product_orders_Product_order_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 <   DROP SEQUENCE public."Product_orders_Product_order_id_seq";
       public          postgres    false    243            �           0    0 #   Product_orders_Product_order_id_seq    SEQUENCE OWNED BY     q   ALTER SEQUENCE public."Product_orders_Product_order_id_seq" OWNED BY public."Product_orders"."Product_order_id";
          public          postgres    false    242            �            1259    16417    Products    TABLE       CREATE TABLE public."Products" (
    "Product_id" integer NOT NULL,
    "Category_id" integer NOT NULL,
    "Product_name" text NOT NULL,
    "Code" text NOT NULL,
    "Buy_cost" money NOT NULL,
    "Sell_cost" money NOT NULL,
    "Barcode" text NOT NULL
);
    DROP TABLE public."Products";
       public         heap    postgres    false            �            1259    16416    Products_Product_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Products_Product_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE public."Products_Product_id_seq";
       public          postgres    false    220            �           0    0    Products_Product_id_seq    SEQUENCE OWNED BY     Y   ALTER SEQUENCE public."Products_Product_id_seq" OWNED BY public."Products"."Product_id";
          public          postgres    false    219            �            1259    16440    Products_in_storage    TABLE     �   CREATE TABLE public."Products_in_storage" (
    "Product_in_storage_id" integer NOT NULL,
    "Shop_id" integer NOT NULL,
    "Product_id" integer NOT NULL,
    "Product_count" integer NOT NULL
);
 )   DROP TABLE public."Products_in_storage";
       public         heap    postgres    false            �            1259    16439 -   Products_in_storage_Product_in_storage_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Products_in_storage_Product_in_storage_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 F   DROP SEQUENCE public."Products_in_storage_Product_in_storage_id_seq";
       public          postgres    false    224            �           0    0 -   Products_in_storage_Product_in_storage_id_seq    SEQUENCE OWNED BY     �   ALTER SEQUENCE public."Products_in_storage_Product_in_storage_id_seq" OWNED BY public."Products_in_storage"."Product_in_storage_id";
          public          postgres    false    223            �            1259    24615    Purchase_product_in_storage    TABLE     �   CREATE TABLE public."Purchase_product_in_storage" (
    "Purchase_id" integer NOT NULL,
    "Products_in_storage_id" integer NOT NULL,
    "Product_count" integer NOT NULL
);
 1   DROP TABLE public."Purchase_product_in_storage";
       public         heap    postgres    false            �            1259    16526 	   Purchases    TABLE     �   CREATE TABLE public."Purchases" (
    "Purchase_id" integer NOT NULL,
    "Date_time" timestamp with time zone NOT NULL,
    "Worker_id" integer NOT NULL
);
    DROP TABLE public."Purchases";
       public         heap    postgres    false            �            1259    16525    Purchases_Purchase_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Purchases_Purchase_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 2   DROP SEQUENCE public."Purchases_Purchase_id_seq";
       public          postgres    false    233            �           0    0    Purchases_Purchase_id_seq    SEQUENCE OWNED BY     ]   ALTER SEQUENCE public."Purchases_Purchase_id_seq" OWNED BY public."Purchases"."Purchase_id";
          public          postgres    false    232            �            1259    24647    Refund_product_in_storage    TABLE     �   CREATE TABLE public."Refund_product_in_storage" (
    "Refund_id" integer NOT NULL,
    "Product_in_storage_id" integer NOT NULL,
    "Product_count" integer NOT NULL
);
 /   DROP TABLE public."Refund_product_in_storage";
       public         heap    postgres    false            �            1259    24631    Refunds    TABLE     �   CREATE TABLE public."Refunds" (
    "Refund_id" integer NOT NULL,
    "Date_Time" time with time zone NOT NULL,
    "Worker_id" integer NOT NULL,
    "Purchase_id" integer NOT NULL
);
    DROP TABLE public."Refunds";
       public         heap    postgres    false            �            1259    24630    Refunds_Refund_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Refunds_Refund_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public."Refunds_Refund_id_seq";
       public          postgres    false    238            �           0    0    Refunds_Refund_id_seq    SEQUENCE OWNED BY     U   ALTER SEQUENCE public."Refunds_Refund_id_seq" OWNED BY public."Refunds"."Refund_id";
          public          postgres    false    237            �            1259    24770    Region_plans    TABLE     �   CREATE TABLE public."Region_plans" (
    "Region_plan_id" integer NOT NULL,
    "Region_id" integer NOT NULL,
    "Turnover" integer NOT NULL,
    "Profit" money NOT NULL,
    "Start_date" date NOT NULL,
    "End_date" date NOT NULL
);
 "   DROP TABLE public."Region_plans";
       public         heap    postgres    false            �            1259    24769    Region_plans_Region_plan_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Region_plans_Region_plan_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 8   DROP SEQUENCE public."Region_plans_Region_plan_id_seq";
       public          postgres    false    254            �           0    0    Region_plans_Region_plan_id_seq    SEQUENCE OWNED BY     i   ALTER SEQUENCE public."Region_plans_Region_plan_id_seq" OWNED BY public."Region_plans"."Region_plan_id";
          public          postgres    false    253            �            1259    24756    Regions    TABLE     e   CREATE TABLE public."Regions" (
    "Region_id" integer NOT NULL,
    "Region_name" text NOT NULL
);
    DROP TABLE public."Regions";
       public         heap    postgres    false            �            1259    24755    Regions_Region_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Regions_Region_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public."Regions_Region_id_seq";
       public          postgres    false    252            �           0    0    Regions_Region_id_seq    SEQUENCE OWNED BY     U   ALTER SEQUENCE public."Regions_Region_id_seq" OWNED BY public."Regions"."Region_id";
          public          postgres    false    251            �            1259    24726    Roles    TABLE     _   CREATE TABLE public."Roles" (
    "Role_id" integer NOT NULL,
    "Role_name" text NOT NULL
);
    DROP TABLE public."Roles";
       public         heap    postgres    false            �            1259    24725    Roles_Role_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Roles_Role_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public."Roles_Role_id_seq";
       public          postgres    false    248            �           0    0    Roles_Role_id_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE public."Roles_Role_id_seq" OWNED BY public."Roles"."Role_id";
          public          postgres    false    247            �            1259    24712    Salary_types    TABLE     t   CREATE TABLE public."Salary_types" (
    "Salary_type_id" integer NOT NULL,
    "Salary_type_name" text NOT NULL
);
 "   DROP TABLE public."Salary_types";
       public         heap    postgres    false            �            1259    24711    Salary_types_Salary_type_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Salary_types_Salary_type_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 8   DROP SEQUENCE public."Salary_types_Salary_type_id_seq";
       public          postgres    false    246            �           0    0    Salary_types_Salary_type_id_seq    SEQUENCE OWNED BY     i   ALTER SEQUENCE public."Salary_types_Salary_type_id_seq" OWNED BY public."Salary_types"."Salary_type_id";
          public          postgres    false    245                        1259    24782 
   Shop_plans    TABLE     �   CREATE TABLE public."Shop_plans" (
    "Shop_plan_id" integer NOT NULL,
    "Shop_id" integer NOT NULL,
    "Turnovet" integer NOT NULL,
    "Profit" money NOT NULL,
    "Start_date" date NOT NULL,
    "End_date" date NOT NULL
);
     DROP TABLE public."Shop_plans";
       public         heap    postgres    false            �            1259    24781    Shop_plans_Shop_plan_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Shop_plans_Shop_plan_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 4   DROP SEQUENCE public."Shop_plans_Shop_plan_id_seq";
       public          postgres    false    256            �           0    0    Shop_plans_Shop_plan_id_seq    SEQUENCE OWNED BY     a   ALTER SEQUENCE public."Shop_plans_Shop_plan_id_seq" OWNED BY public."Shop_plans"."Shop_plan_id";
          public          postgres    false    255                       1259    24794    Shop_positions    TABLE     �   CREATE TABLE public."Shop_positions" (
    "Shop_position_id" integer NOT NULL,
    "Shop_id" integer NOT NULL,
    "Position_id" integer NOT NULL,
    "Worker_id" integer,
    "Salary" money
);
 $   DROP TABLE public."Shop_positions";
       public         heap    postgres    false                       1259    24793 #   Shop_positions_Shop_position_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Shop_positions_Shop_position_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 <   DROP SEQUENCE public."Shop_positions_Shop_position_id_seq";
       public          postgres    false    258            �           0    0 #   Shop_positions_Shop_position_id_seq    SEQUENCE OWNED BY     q   ALTER SEQUENCE public."Shop_positions_Shop_position_id_seq" OWNED BY public."Shop_positions"."Shop_position_id";
          public          postgres    false    257            �            1259    24740 
   Shop_types    TABLE     n   CREATE TABLE public."Shop_types" (
    "Shop_type_id" integer NOT NULL,
    "Shop_type_name" text NOT NULL
);
     DROP TABLE public."Shop_types";
       public         heap    postgres    false            �            1259    24739    Shop_types_Shop_type_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Shop_types_Shop_type_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 4   DROP SEQUENCE public."Shop_types_Shop_type_id_seq";
       public          postgres    false    250            �           0    0    Shop_types_Shop_type_id_seq    SEQUENCE OWNED BY     a   ALTER SEQUENCE public."Shop_types_Shop_type_id_seq" OWNED BY public."Shop_types"."Shop_type_id";
          public          postgres    false    249            �            1259    16399    Shops    TABLE     �   CREATE TABLE public."Shops" (
    "Shop_id" integer NOT NULL,
    "Addres" text NOT NULL,
    "Shop_type_id" integer NOT NULL,
    "Region_id" integer NOT NULL
);
    DROP TABLE public."Shops";
       public         heap    postgres    false            �            1259    16398    Shops_Shop_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Shops_Shop_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public."Shops_Shop_id_seq";
       public          postgres    false    216            �           0    0    Shops_Shop_id_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE public."Shops_Shop_id_seq" OWNED BY public."Shops"."Shop_id";
          public          postgres    false    215                       1259    40985 	   TestTable    TABLE     a   CREATE TABLE public."TestTable" (
    "TestId" integer NOT NULL,
    "TestText" text NOT NULL
);
    DROP TABLE public."TestTable";
       public         heap    postgres    false                       1259    40984    TestTable_TestId_seq    SEQUENCE     �   CREATE SEQUENCE public."TestTable_TestId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 -   DROP SEQUENCE public."TestTable_TestId_seq";
       public          postgres    false    260            �           0    0    TestTable_TestId_seq    SEQUENCE OWNED BY     S   ALTER SEQUENCE public."TestTable_TestId_seq" OWNED BY public."TestTable"."TestId";
          public          postgres    false    259            �            1259    24601    Worker_types    TABLE     t   CREATE TABLE public."Worker_types" (
    "Worker_type_id" integer NOT NULL,
    "Worker_type_name" text NOT NULL
);
 "   DROP TABLE public."Worker_types";
       public         heap    postgres    false            �            1259    24600    Worker_types_Worker_type_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Worker_types_Worker_type_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 8   DROP SEQUENCE public."Worker_types_Worker_type_id_seq";
       public          postgres    false    235            �           0    0    Worker_types_Worker_type_id_seq    SEQUENCE OWNED BY     i   ALTER SEQUENCE public."Worker_types_Worker_type_id_seq" OWNED BY public."Worker_types"."Worker_type_id";
          public          postgres    false    234            �            1259    16431    Workers    TABLE     �   CREATE TABLE public."Workers" (
    "Worker_id" integer NOT NULL,
    "Fullname" text NOT NULL,
    "Worker_type_id" integer DEFAULT 1 NOT NULL
);
    DROP TABLE public."Workers";
       public         heap    postgres    false            �            1259    16430    Workers_Worker_id_seq    SEQUENCE     �   CREATE SEQUENCE public."Workers_Worker_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public."Workers_Worker_id_seq";
       public          postgres    false    222            �           0    0    Workers_Worker_id_seq    SEQUENCE OWNED BY     U   ALTER SEQUENCE public."Workers_Worker_id_seq" OWNED BY public."Workers"."Worker_id";
          public          postgres    false    221            �           2604    16411    Categories Category_id    DEFAULT     �   ALTER TABLE ONLY public."Categories" ALTER COLUMN "Category_id" SET DEFAULT nextval('public."Categories_Category_id_seq"'::regclass);
 I   ALTER TABLE public."Categories" ALTER COLUMN "Category_id" DROP DEFAULT;
       public          postgres    false    217    218    218            �           2604    24666 '   Order_consignments Order_consignment_id    DEFAULT     �   ALTER TABLE ONLY public."Order_consignments" ALTER COLUMN "Order_consignment_id" SET DEFAULT nextval('public."Accepting_orders_Consignment_id_seq"'::regclass);
 Z   ALTER TABLE public."Order_consignments" ALTER COLUMN "Order_consignment_id" DROP DEFAULT;
       public          postgres    false    240    241    241            �           2604    16497    Payments Payment_id    DEFAULT     �   ALTER TABLE ONLY public."Payments" ALTER COLUMN "Payment_id" SET DEFAULT nextval('public."Payments_Payment_id_seq"'::regclass);
 F   ALTER TABLE public."Payments" ALTER COLUMN "Payment_id" DROP DEFAULT;
       public          postgres    false    231    230    231            �           2604    16488    Positions Position_id    DEFAULT     �   ALTER TABLE ONLY public."Positions" ALTER COLUMN "Position_id" SET DEFAULT nextval('public."Positions_Position_id_seq"'::regclass);
 H   ALTER TABLE public."Positions" ALTER COLUMN "Position_id" DROP DEFAULT;
       public          postgres    false    228    229    229            �           2604    16460 +   Product_consignments Product_consignment_id    DEFAULT     �   ALTER TABLE ONLY public."Product_consignments" ALTER COLUMN "Product_consignment_id" SET DEFAULT nextval('public."Product_consignments_Product_consignment_id_seq"'::regclass);
 ^   ALTER TABLE public."Product_consignments" ALTER COLUMN "Product_consignment_id" DROP DEFAULT;
       public          postgres    false    225    226    226            �           2604    24678    Product_orders Product_order_id    DEFAULT     �   ALTER TABLE ONLY public."Product_orders" ALTER COLUMN "Product_order_id" SET DEFAULT nextval('public."Product_orders_Product_order_id_seq"'::regclass);
 R   ALTER TABLE public."Product_orders" ALTER COLUMN "Product_order_id" DROP DEFAULT;
       public          postgres    false    242    243    243            �           2604    16420    Products Product_id    DEFAULT     �   ALTER TABLE ONLY public."Products" ALTER COLUMN "Product_id" SET DEFAULT nextval('public."Products_Product_id_seq"'::regclass);
 F   ALTER TABLE public."Products" ALTER COLUMN "Product_id" DROP DEFAULT;
       public          postgres    false    220    219    220            �           2604    16443 )   Products_in_storage Product_in_storage_id    DEFAULT     �   ALTER TABLE ONLY public."Products_in_storage" ALTER COLUMN "Product_in_storage_id" SET DEFAULT nextval('public."Products_in_storage_Product_in_storage_id_seq"'::regclass);
 \   ALTER TABLE public."Products_in_storage" ALTER COLUMN "Product_in_storage_id" DROP DEFAULT;
       public          postgres    false    224    223    224            �           2604    16529    Purchases Purchase_id    DEFAULT     �   ALTER TABLE ONLY public."Purchases" ALTER COLUMN "Purchase_id" SET DEFAULT nextval('public."Purchases_Purchase_id_seq"'::regclass);
 H   ALTER TABLE public."Purchases" ALTER COLUMN "Purchase_id" DROP DEFAULT;
       public          postgres    false    232    233    233            �           2604    24634    Refunds Refund_id    DEFAULT     |   ALTER TABLE ONLY public."Refunds" ALTER COLUMN "Refund_id" SET DEFAULT nextval('public."Refunds_Refund_id_seq"'::regclass);
 D   ALTER TABLE public."Refunds" ALTER COLUMN "Refund_id" DROP DEFAULT;
       public          postgres    false    237    238    238            �           2604    24773    Region_plans Region_plan_id    DEFAULT     �   ALTER TABLE ONLY public."Region_plans" ALTER COLUMN "Region_plan_id" SET DEFAULT nextval('public."Region_plans_Region_plan_id_seq"'::regclass);
 N   ALTER TABLE public."Region_plans" ALTER COLUMN "Region_plan_id" DROP DEFAULT;
       public          postgres    false    254    253    254            �           2604    24759    Regions Region_id    DEFAULT     |   ALTER TABLE ONLY public."Regions" ALTER COLUMN "Region_id" SET DEFAULT nextval('public."Regions_Region_id_seq"'::regclass);
 D   ALTER TABLE public."Regions" ALTER COLUMN "Region_id" DROP DEFAULT;
       public          postgres    false    251    252    252            �           2604    24729    Roles Role_id    DEFAULT     t   ALTER TABLE ONLY public."Roles" ALTER COLUMN "Role_id" SET DEFAULT nextval('public."Roles_Role_id_seq"'::regclass);
 @   ALTER TABLE public."Roles" ALTER COLUMN "Role_id" DROP DEFAULT;
       public          postgres    false    247    248    248            �           2604    24715    Salary_types Salary_type_id    DEFAULT     �   ALTER TABLE ONLY public."Salary_types" ALTER COLUMN "Salary_type_id" SET DEFAULT nextval('public."Salary_types_Salary_type_id_seq"'::regclass);
 N   ALTER TABLE public."Salary_types" ALTER COLUMN "Salary_type_id" DROP DEFAULT;
       public          postgres    false    245    246    246            �           2604    24785    Shop_plans Shop_plan_id    DEFAULT     �   ALTER TABLE ONLY public."Shop_plans" ALTER COLUMN "Shop_plan_id" SET DEFAULT nextval('public."Shop_plans_Shop_plan_id_seq"'::regclass);
 J   ALTER TABLE public."Shop_plans" ALTER COLUMN "Shop_plan_id" DROP DEFAULT;
       public          postgres    false    255    256    256            �           2604    24797    Shop_positions Shop_position_id    DEFAULT     �   ALTER TABLE ONLY public."Shop_positions" ALTER COLUMN "Shop_position_id" SET DEFAULT nextval('public."Shop_positions_Shop_position_id_seq"'::regclass);
 R   ALTER TABLE public."Shop_positions" ALTER COLUMN "Shop_position_id" DROP DEFAULT;
       public          postgres    false    258    257    258            �           2604    24743    Shop_types Shop_type_id    DEFAULT     �   ALTER TABLE ONLY public."Shop_types" ALTER COLUMN "Shop_type_id" SET DEFAULT nextval('public."Shop_types_Shop_type_id_seq"'::regclass);
 J   ALTER TABLE public."Shop_types" ALTER COLUMN "Shop_type_id" DROP DEFAULT;
       public          postgres    false    249    250    250            �           2604    16402    Shops Shop_id    DEFAULT     t   ALTER TABLE ONLY public."Shops" ALTER COLUMN "Shop_id" SET DEFAULT nextval('public."Shops_Shop_id_seq"'::regclass);
 @   ALTER TABLE public."Shops" ALTER COLUMN "Shop_id" DROP DEFAULT;
       public          postgres    false    216    215    216            �           2604    40988    TestTable TestId    DEFAULT     z   ALTER TABLE ONLY public."TestTable" ALTER COLUMN "TestId" SET DEFAULT nextval('public."TestTable_TestId_seq"'::regclass);
 C   ALTER TABLE public."TestTable" ALTER COLUMN "TestId" DROP DEFAULT;
       public          postgres    false    260    259    260            �           2604    24604    Worker_types Worker_type_id    DEFAULT     �   ALTER TABLE ONLY public."Worker_types" ALTER COLUMN "Worker_type_id" SET DEFAULT nextval('public."Worker_types_Worker_type_id_seq"'::regclass);
 N   ALTER TABLE public."Worker_types" ALTER COLUMN "Worker_type_id" DROP DEFAULT;
       public          postgres    false    234    235    235            �           2604    16434    Workers Worker_id    DEFAULT     |   ALTER TABLE ONLY public."Workers" ALTER COLUMN "Worker_id" SET DEFAULT nextval('public."Workers_Worker_id_seq"'::regclass);
 D   ALTER TABLE public."Workers" ALTER COLUMN "Worker_id" DROP DEFAULT;
       public          postgres    false    221    222    222            �          0    16408 
   Categories 
   TABLE DATA           F   COPY public."Categories" ("Category_id", "Category_name") FROM stdin;
    public          postgres    false    218   �      �          0    24663    Order_consignments 
   TABLE DATA           `   COPY public."Order_consignments" ("Order_consignment_id", "Worker_id", "Date_time") FROM stdin;
    public          postgres    false    241   �      �          0    16494    Payments 
   TABLE DATA           �   COPY public."Payments" ("Payment_id", "Shop_id", "Approvier_worker_id", "Recipient_worker_id", "Amount", "Is_approved", "Comment") FROM stdin;
    public          postgres    false    231   �      �          0    16485 	   Positions 
   TABLE DATA           b   COPY public."Positions" ("Position_id", "Position_name", "Salary_type_id", "Role_id") FROM stdin;
    public          postgres    false    229   
      �          0    16468    Product_consignment_product 
   TABLE DATA           h   COPY public."Product_consignment_product" ("Product_id", "Product_consignment_id", "Count") FROM stdin;
    public          postgres    false    227   '      �          0    16457    Product_consignments 
   TABLE DATA           |   COPY public."Product_consignments" ("Product_consignment_id", "Worker_id", "Date_time", "Order_consignment_id") FROM stdin;
    public          postgres    false    226   D      �          0    24691     Product_order_product_in_storage 
   TABLE DATA           z   COPY public."Product_order_product_in_storage" ("Product_order_id", "Product_in_storage_id", "Product_count") FROM stdin;
    public          postgres    false    244   a      �          0    24675    Product_orders 
   TABLE DATA              COPY public."Product_orders" ("Product_order_id", "Date_time", "Worker_id", "Order_consignment_id", "Is_approved") FROM stdin;
    public          postgres    false    243   ~      �          0    16417    Products 
   TABLE DATA           }   COPY public."Products" ("Product_id", "Category_id", "Product_name", "Code", "Buy_cost", "Sell_cost", "Barcode") FROM stdin;
    public          postgres    false    220   �      �          0    16440    Products_in_storage 
   TABLE DATA           r   COPY public."Products_in_storage" ("Product_in_storage_id", "Shop_id", "Product_id", "Product_count") FROM stdin;
    public          postgres    false    224   �      �          0    24615    Purchase_product_in_storage 
   TABLE DATA           q   COPY public."Purchase_product_in_storage" ("Purchase_id", "Products_in_storage_id", "Product_count") FROM stdin;
    public          postgres    false    236   �      �          0    16526 	   Purchases 
   TABLE DATA           N   COPY public."Purchases" ("Purchase_id", "Date_time", "Worker_id") FROM stdin;
    public          postgres    false    233   �      �          0    24647    Refund_product_in_storage 
   TABLE DATA           l   COPY public."Refund_product_in_storage" ("Refund_id", "Product_in_storage_id", "Product_count") FROM stdin;
    public          postgres    false    239         �          0    24631    Refunds 
   TABLE DATA           Y   COPY public."Refunds" ("Refund_id", "Date_Time", "Worker_id", "Purchase_id") FROM stdin;
    public          postgres    false    238   ,      �          0    24770    Region_plans 
   TABLE DATA           w   COPY public."Region_plans" ("Region_plan_id", "Region_id", "Turnover", "Profit", "Start_date", "End_date") FROM stdin;
    public          postgres    false    254   I      �          0    24756    Regions 
   TABLE DATA           ?   COPY public."Regions" ("Region_id", "Region_name") FROM stdin;
    public          postgres    false    252   f      �          0    24726    Roles 
   TABLE DATA           9   COPY public."Roles" ("Role_id", "Role_name") FROM stdin;
    public          postgres    false    248   �      �          0    24712    Salary_types 
   TABLE DATA           N   COPY public."Salary_types" ("Salary_type_id", "Salary_type_name") FROM stdin;
    public          postgres    false    246   �      �          0    24782 
   Shop_plans 
   TABLE DATA           q   COPY public."Shop_plans" ("Shop_plan_id", "Shop_id", "Turnovet", "Profit", "Start_date", "End_date") FROM stdin;
    public          postgres    false    256   �      �          0    24794    Shop_positions 
   TABLE DATA           o   COPY public."Shop_positions" ("Shop_position_id", "Shop_id", "Position_id", "Worker_id", "Salary") FROM stdin;
    public          postgres    false    258   �      �          0    24740 
   Shop_types 
   TABLE DATA           H   COPY public."Shop_types" ("Shop_type_id", "Shop_type_name") FROM stdin;
    public          postgres    false    250   �      �          0    16399    Shops 
   TABLE DATA           S   COPY public."Shops" ("Shop_id", "Addres", "Shop_type_id", "Region_id") FROM stdin;
    public          postgres    false    216         �          0    40985 	   TestTable 
   TABLE DATA           ;   COPY public."TestTable" ("TestId", "TestText") FROM stdin;
    public          postgres    false    260   1      �          0    24601    Worker_types 
   TABLE DATA           N   COPY public."Worker_types" ("Worker_type_id", "Worker_type_name") FROM stdin;
    public          postgres    false    235   N      �          0    16431    Workers 
   TABLE DATA           N   COPY public."Workers" ("Worker_id", "Fullname", "Worker_type_id") FROM stdin;
    public          postgres    false    222   }      �           0    0 #   Accepting_orders_Consignment_id_seq    SEQUENCE SET     T   SELECT pg_catalog.setval('public."Accepting_orders_Consignment_id_seq"', 1, false);
          public          postgres    false    240            �           0    0    Categories_Category_id_seq    SEQUENCE SET     J   SELECT pg_catalog.setval('public."Categories_Category_id_seq"', 1, true);
          public          postgres    false    217            �           0    0    Payments_Payment_id_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('public."Payments_Payment_id_seq"', 1, false);
          public          postgres    false    230            �           0    0    Positions_Position_id_seq    SEQUENCE SET     J   SELECT pg_catalog.setval('public."Positions_Position_id_seq"', 1, false);
          public          postgres    false    228            �           0    0 /   Product_consignments_Product_consignment_id_seq    SEQUENCE SET     `   SELECT pg_catalog.setval('public."Product_consignments_Product_consignment_id_seq"', 1, false);
          public          postgres    false    225            �           0    0 #   Product_orders_Product_order_id_seq    SEQUENCE SET     T   SELECT pg_catalog.setval('public."Product_orders_Product_order_id_seq"', 1, false);
          public          postgres    false    242            �           0    0    Products_Product_id_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('public."Products_Product_id_seq"', 1, false);
          public          postgres    false    219            �           0    0 -   Products_in_storage_Product_in_storage_id_seq    SEQUENCE SET     ^   SELECT pg_catalog.setval('public."Products_in_storage_Product_in_storage_id_seq"', 1, false);
          public          postgres    false    223            �           0    0    Purchases_Purchase_id_seq    SEQUENCE SET     J   SELECT pg_catalog.setval('public."Purchases_Purchase_id_seq"', 1, false);
          public          postgres    false    232            �           0    0    Refunds_Refund_id_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public."Refunds_Refund_id_seq"', 1, false);
          public          postgres    false    237            �           0    0    Region_plans_Region_plan_id_seq    SEQUENCE SET     P   SELECT pg_catalog.setval('public."Region_plans_Region_plan_id_seq"', 1, false);
          public          postgres    false    253            �           0    0    Regions_Region_id_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public."Regions_Region_id_seq"', 1, false);
          public          postgres    false    251            �           0    0    Roles_Role_id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public."Roles_Role_id_seq"', 1, false);
          public          postgres    false    247            �           0    0    Salary_types_Salary_type_id_seq    SEQUENCE SET     P   SELECT pg_catalog.setval('public."Salary_types_Salary_type_id_seq"', 1, false);
          public          postgres    false    245            �           0    0    Shop_plans_Shop_plan_id_seq    SEQUENCE SET     L   SELECT pg_catalog.setval('public."Shop_plans_Shop_plan_id_seq"', 1, false);
          public          postgres    false    255            �           0    0 #   Shop_positions_Shop_position_id_seq    SEQUENCE SET     T   SELECT pg_catalog.setval('public."Shop_positions_Shop_position_id_seq"', 1, false);
          public          postgres    false    257            �           0    0    Shop_types_Shop_type_id_seq    SEQUENCE SET     L   SELECT pg_catalog.setval('public."Shop_types_Shop_type_id_seq"', 1, false);
          public          postgres    false    249            �           0    0    Shops_Shop_id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public."Shops_Shop_id_seq"', 1, false);
          public          postgres    false    215            �           0    0    TestTable_TestId_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public."TestTable_TestId_seq"', 1, false);
          public          postgres    false    259            �           0    0    Worker_types_Worker_type_id_seq    SEQUENCE SET     O   SELECT pg_catalog.setval('public."Worker_types_Worker_type_id_seq"', 2, true);
          public          postgres    false    234            �           0    0    Workers_Worker_id_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public."Workers_Worker_id_seq"', 1, false);
          public          postgres    false    221            �           2606    24668 (   Order_consignments Accepting_orders_pkey 
   CONSTRAINT     ~   ALTER TABLE ONLY public."Order_consignments"
    ADD CONSTRAINT "Accepting_orders_pkey" PRIMARY KEY ("Order_consignment_id");
 V   ALTER TABLE ONLY public."Order_consignments" DROP CONSTRAINT "Accepting_orders_pkey";
       public            postgres    false    241            �           2606    16415    Categories Categories_pkey 
   CONSTRAINT     g   ALTER TABLE ONLY public."Categories"
    ADD CONSTRAINT "Categories_pkey" PRIMARY KEY ("Category_id");
 H   ALTER TABLE ONLY public."Categories" DROP CONSTRAINT "Categories_pkey";
       public            postgres    false    218            �           2606    16499    Payments Payments_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public."Payments"
    ADD CONSTRAINT "Payments_pkey" PRIMARY KEY ("Payment_id");
 D   ALTER TABLE ONLY public."Payments" DROP CONSTRAINT "Payments_pkey";
       public            postgres    false    231            �           2606    16492    Positions Positions_pkey 
   CONSTRAINT     e   ALTER TABLE ONLY public."Positions"
    ADD CONSTRAINT "Positions_pkey" PRIMARY KEY ("Position_id");
 F   ALTER TABLE ONLY public."Positions" DROP CONSTRAINT "Positions_pkey";
       public            postgres    false    229            �           2606    16472 <   Product_consignment_product Product_consignment_product_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Product_consignment_product"
    ADD CONSTRAINT "Product_consignment_product_pkey" PRIMARY KEY ("Product_id", "Product_consignment_id");
 j   ALTER TABLE ONLY public."Product_consignment_product" DROP CONSTRAINT "Product_consignment_product_pkey";
       public            postgres    false    227    227            �           2606    16462 .   Product_consignments Product_consignments_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Product_consignments"
    ADD CONSTRAINT "Product_consignments_pkey" PRIMARY KEY ("Product_consignment_id");
 \   ALTER TABLE ONLY public."Product_consignments" DROP CONSTRAINT "Product_consignments_pkey";
       public            postgres    false    226            �           2606    24695 F   Product_order_product_in_storage Product_order_product_in_storage_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Product_order_product_in_storage"
    ADD CONSTRAINT "Product_order_product_in_storage_pkey" PRIMARY KEY ("Product_order_id", "Product_in_storage_id");
 t   ALTER TABLE ONLY public."Product_order_product_in_storage" DROP CONSTRAINT "Product_order_product_in_storage_pkey";
       public            postgres    false    244    244            �           2606    24680 "   Product_orders Product_orders_pkey 
   CONSTRAINT     t   ALTER TABLE ONLY public."Product_orders"
    ADD CONSTRAINT "Product_orders_pkey" PRIMARY KEY ("Product_order_id");
 P   ALTER TABLE ONLY public."Product_orders" DROP CONSTRAINT "Product_orders_pkey";
       public            postgres    false    243            �           2606    16445 ,   Products_in_storage Products_in_storage_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Products_in_storage"
    ADD CONSTRAINT "Products_in_storage_pkey" PRIMARY KEY ("Product_in_storage_id");
 Z   ALTER TABLE ONLY public."Products_in_storage" DROP CONSTRAINT "Products_in_storage_pkey";
       public            postgres    false    224            �           2606    16424    Products Products_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public."Products"
    ADD CONSTRAINT "Products_pkey" PRIMARY KEY ("Product_id");
 D   ALTER TABLE ONLY public."Products" DROP CONSTRAINT "Products_pkey";
       public            postgres    false    220            �           2606    24619 <   Purchase_product_in_storage Purchase_product_in_storage_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Purchase_product_in_storage"
    ADD CONSTRAINT "Purchase_product_in_storage_pkey" PRIMARY KEY ("Purchase_id", "Products_in_storage_id");
 j   ALTER TABLE ONLY public."Purchase_product_in_storage" DROP CONSTRAINT "Purchase_product_in_storage_pkey";
       public            postgres    false    236    236            �           2606    16531    Purchases Purchases_pkey 
   CONSTRAINT     e   ALTER TABLE ONLY public."Purchases"
    ADD CONSTRAINT "Purchases_pkey" PRIMARY KEY ("Purchase_id");
 F   ALTER TABLE ONLY public."Purchases" DROP CONSTRAINT "Purchases_pkey";
       public            postgres    false    233            �           2606    24651 8   Refund_product_in_storage Refund_product_in_storage_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Refund_product_in_storage"
    ADD CONSTRAINT "Refund_product_in_storage_pkey" PRIMARY KEY ("Refund_id", "Product_in_storage_id");
 f   ALTER TABLE ONLY public."Refund_product_in_storage" DROP CONSTRAINT "Refund_product_in_storage_pkey";
       public            postgres    false    239    239            �           2606    24636    Refunds Refunds_pkey 
   CONSTRAINT     _   ALTER TABLE ONLY public."Refunds"
    ADD CONSTRAINT "Refunds_pkey" PRIMARY KEY ("Refund_id");
 B   ALTER TABLE ONLY public."Refunds" DROP CONSTRAINT "Refunds_pkey";
       public            postgres    false    238            �           2606    24775    Region_plans Region_plans_pkey 
   CONSTRAINT     n   ALTER TABLE ONLY public."Region_plans"
    ADD CONSTRAINT "Region_plans_pkey" PRIMARY KEY ("Region_plan_id");
 L   ALTER TABLE ONLY public."Region_plans" DROP CONSTRAINT "Region_plans_pkey";
       public            postgres    false    254            �           2606    24763    Regions Regions_pkey 
   CONSTRAINT     _   ALTER TABLE ONLY public."Regions"
    ADD CONSTRAINT "Regions_pkey" PRIMARY KEY ("Region_id");
 B   ALTER TABLE ONLY public."Regions" DROP CONSTRAINT "Regions_pkey";
       public            postgres    false    252            �           2606    24733    Roles Roles_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public."Roles"
    ADD CONSTRAINT "Roles_pkey" PRIMARY KEY ("Role_id");
 >   ALTER TABLE ONLY public."Roles" DROP CONSTRAINT "Roles_pkey";
       public            postgres    false    248            �           2606    24719    Salary_types Salary_types_pkey 
   CONSTRAINT     n   ALTER TABLE ONLY public."Salary_types"
    ADD CONSTRAINT "Salary_types_pkey" PRIMARY KEY ("Salary_type_id");
 L   ALTER TABLE ONLY public."Salary_types" DROP CONSTRAINT "Salary_types_pkey";
       public            postgres    false    246            �           2606    24787    Shop_plans Shop_plans_pkey 
   CONSTRAINT     h   ALTER TABLE ONLY public."Shop_plans"
    ADD CONSTRAINT "Shop_plans_pkey" PRIMARY KEY ("Shop_plan_id");
 H   ALTER TABLE ONLY public."Shop_plans" DROP CONSTRAINT "Shop_plans_pkey";
       public            postgres    false    256            �           2606    24799 "   Shop_positions Shop_positions_pkey 
   CONSTRAINT     t   ALTER TABLE ONLY public."Shop_positions"
    ADD CONSTRAINT "Shop_positions_pkey" PRIMARY KEY ("Shop_position_id");
 P   ALTER TABLE ONLY public."Shop_positions" DROP CONSTRAINT "Shop_positions_pkey";
       public            postgres    false    258            �           2606    24749    Shop_types Shop_types_pkey 
   CONSTRAINT     h   ALTER TABLE ONLY public."Shop_types"
    ADD CONSTRAINT "Shop_types_pkey" PRIMARY KEY ("Shop_type_id");
 H   ALTER TABLE ONLY public."Shop_types" DROP CONSTRAINT "Shop_types_pkey";
       public            postgres    false    250            �           2606    16406    Shops Shops_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public."Shops"
    ADD CONSTRAINT "Shops_pkey" PRIMARY KEY ("Shop_id");
 >   ALTER TABLE ONLY public."Shops" DROP CONSTRAINT "Shops_pkey";
       public            postgres    false    216            �           2606    40992    TestTable TestTable_pkey 
   CONSTRAINT     `   ALTER TABLE ONLY public."TestTable"
    ADD CONSTRAINT "TestTable_pkey" PRIMARY KEY ("TestId");
 F   ALTER TABLE ONLY public."TestTable" DROP CONSTRAINT "TestTable_pkey";
       public            postgres    false    260            �           2606    24608    Worker_types Worker_types_pkey 
   CONSTRAINT     n   ALTER TABLE ONLY public."Worker_types"
    ADD CONSTRAINT "Worker_types_pkey" PRIMARY KEY ("Worker_type_id");
 L   ALTER TABLE ONLY public."Worker_types" DROP CONSTRAINT "Worker_types_pkey";
       public            postgres    false    235            �           2606    16438    Workers Workers_pkey 
   CONSTRAINT     _   ALTER TABLE ONLY public."Workers"
    ADD CONSTRAINT "Workers_pkey" PRIMARY KEY ("Worker_id");
 B   ALTER TABLE ONLY public."Workers" DROP CONSTRAINT "Workers_pkey";
       public            postgres    false    222            �           2606    24669 2   Order_consignments Accepting_orders_Worker_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Order_consignments"
    ADD CONSTRAINT "Accepting_orders_Worker_id_fkey" FOREIGN KEY ("Worker_id") REFERENCES public."Workers"("Worker_id");
 `   ALTER TABLE ONLY public."Order_consignments" DROP CONSTRAINT "Accepting_orders_Worker_id_fkey";
       public          postgres    false    241    4780    222            �           2606    16515 *   Payments Payments_Approvier_worker_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Payments"
    ADD CONSTRAINT "Payments_Approvier_worker_id_fkey" FOREIGN KEY ("Approvier_worker_id") REFERENCES public."Workers"("Worker_id") NOT VALID;
 X   ALTER TABLE ONLY public."Payments" DROP CONSTRAINT "Payments_Approvier_worker_id_fkey";
       public          postgres    false    4780    222    231            �           2606    16520 *   Payments Payments_Recipient_worker_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Payments"
    ADD CONSTRAINT "Payments_Recipient_worker_id_fkey" FOREIGN KEY ("Recipient_worker_id") REFERENCES public."Workers"("Worker_id") NOT VALID;
 X   ALTER TABLE ONLY public."Payments" DROP CONSTRAINT "Payments_Recipient_worker_id_fkey";
       public          postgres    false    4780    231    222            �           2606    16510    Payments Payments_Shop_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Payments"
    ADD CONSTRAINT "Payments_Shop_id_fkey" FOREIGN KEY ("Shop_id") REFERENCES public."Shops"("Shop_id") NOT VALID;
 L   ALTER TABLE ONLY public."Payments" DROP CONSTRAINT "Payments_Shop_id_fkey";
       public          postgres    false    4774    216    231            �           2606    24734     Positions Positions_Role_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Positions"
    ADD CONSTRAINT "Positions_Role_id_fkey" FOREIGN KEY ("Role_id") REFERENCES public."Roles"("Role_id") NOT VALID;
 N   ALTER TABLE ONLY public."Positions" DROP CONSTRAINT "Positions_Role_id_fkey";
       public          postgres    false    229    248    4810            �           2606    24720 '   Positions Positions_Salary_type_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Positions"
    ADD CONSTRAINT "Positions_Salary_type_id_fkey" FOREIGN KEY ("Salary_type_id") REFERENCES public."Salary_types"("Salary_type_id") NOT VALID;
 U   ALTER TABLE ONLY public."Positions" DROP CONSTRAINT "Positions_Salary_type_id_fkey";
       public          postgres    false    229    4808    246            �           2606    16478 S   Product_consignment_product Product_consignment_product_Product_consignment_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Product_consignment_product"
    ADD CONSTRAINT "Product_consignment_product_Product_consignment_id_fkey" FOREIGN KEY ("Product_consignment_id") REFERENCES public."Product_consignments"("Product_consignment_id") NOT VALID;
 �   ALTER TABLE ONLY public."Product_consignment_product" DROP CONSTRAINT "Product_consignment_product_Product_consignment_id_fkey";
       public          postgres    false    227    4784    226            �           2606    16473 G   Product_consignment_product Product_consignment_product_Product_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Product_consignment_product"
    ADD CONSTRAINT "Product_consignment_product_Product_id_fkey" FOREIGN KEY ("Product_id") REFERENCES public."Products"("Product_id") NOT VALID;
 u   ALTER TABLE ONLY public."Product_consignment_product" DROP CONSTRAINT "Product_consignment_product_Product_id_fkey";
       public          postgres    false    227    220    4778            �           2606    24706 C   Product_consignments Product_consignments_Order_consignment_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Product_consignments"
    ADD CONSTRAINT "Product_consignments_Order_consignment_id_fkey" FOREIGN KEY ("Order_consignment_id") REFERENCES public."Order_consignments"("Order_consignment_id") NOT VALID;
 q   ALTER TABLE ONLY public."Product_consignments" DROP CONSTRAINT "Product_consignments_Order_consignment_id_fkey";
       public          postgres    false    4802    226    241            �           2606    16463 8   Product_consignments Product_consignments_Worker_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Product_consignments"
    ADD CONSTRAINT "Product_consignments_Worker_id_fkey" FOREIGN KEY ("Worker_id") REFERENCES public."Workers"("Worker_id");
 f   ALTER TABLE ONLY public."Product_consignments" DROP CONSTRAINT "Product_consignments_Worker_id_fkey";
       public          postgres    false    222    226    4780            �           2606    24701 \   Product_order_product_in_storage Product_order_product_in_storage_Product_in_storage_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Product_order_product_in_storage"
    ADD CONSTRAINT "Product_order_product_in_storage_Product_in_storage_id_fkey" FOREIGN KEY ("Product_in_storage_id") REFERENCES public."Products_in_storage"("Product_in_storage_id");
 �   ALTER TABLE ONLY public."Product_order_product_in_storage" DROP CONSTRAINT "Product_order_product_in_storage_Product_in_storage_id_fkey";
       public          postgres    false    244    4782    224            �           2606    24696 W   Product_order_product_in_storage Product_order_product_in_storage_Product_order_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Product_order_product_in_storage"
    ADD CONSTRAINT "Product_order_product_in_storage_Product_order_id_fkey" FOREIGN KEY ("Product_order_id") REFERENCES public."Product_orders"("Product_order_id");
 �   ALTER TABLE ONLY public."Product_order_product_in_storage" DROP CONSTRAINT "Product_order_product_in_storage_Product_order_id_fkey";
       public          postgres    false    243    244    4804            �           2606    24686 0   Product_orders Product_orders_Consigment_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Product_orders"
    ADD CONSTRAINT "Product_orders_Consigment_id_fkey" FOREIGN KEY ("Order_consignment_id") REFERENCES public."Order_consignments"("Order_consignment_id");
 ^   ALTER TABLE ONLY public."Product_orders" DROP CONSTRAINT "Product_orders_Consigment_id_fkey";
       public          postgres    false    241    243    4802            �           2606    24681 ,   Product_orders Product_orders_Worker_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Product_orders"
    ADD CONSTRAINT "Product_orders_Worker_id_fkey" FOREIGN KEY ("Worker_id") REFERENCES public."Workers"("Worker_id");
 Z   ALTER TABLE ONLY public."Product_orders" DROP CONSTRAINT "Product_orders_Worker_id_fkey";
       public          postgres    false    243    4780    222            �           2606    16425 "   Products Products_Category_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Products"
    ADD CONSTRAINT "Products_Category_id_fkey" FOREIGN KEY ("Category_id") REFERENCES public."Categories"("Category_id");
 P   ALTER TABLE ONLY public."Products" DROP CONSTRAINT "Products_Category_id_fkey";
       public          postgres    false    220    218    4776            �           2606    16451 7   Products_in_storage Products_in_storage_Product_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Products_in_storage"
    ADD CONSTRAINT "Products_in_storage_Product_id_fkey" FOREIGN KEY ("Product_id") REFERENCES public."Products"("Product_id") NOT VALID;
 e   ALTER TABLE ONLY public."Products_in_storage" DROP CONSTRAINT "Products_in_storage_Product_id_fkey";
       public          postgres    false    4778    224    220            �           2606    16446 4   Products_in_storage Products_in_storage_Shop_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Products_in_storage"
    ADD CONSTRAINT "Products_in_storage_Shop_id_fkey" FOREIGN KEY ("Shop_id") REFERENCES public."Shops"("Shop_id") NOT VALID;
 b   ALTER TABLE ONLY public."Products_in_storage" DROP CONSTRAINT "Products_in_storage_Shop_id_fkey";
       public          postgres    false    4774    216    224            �           2606    24625 S   Purchase_product_in_storage Purchase_product_in_storage_Products_in_storage_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Purchase_product_in_storage"
    ADD CONSTRAINT "Purchase_product_in_storage_Products_in_storage_id_fkey" FOREIGN KEY ("Products_in_storage_id") REFERENCES public."Products_in_storage"("Product_in_storage_id");
 �   ALTER TABLE ONLY public."Purchase_product_in_storage" DROP CONSTRAINT "Purchase_product_in_storage_Products_in_storage_id_fkey";
       public          postgres    false    224    236    4782            �           2606    24620 H   Purchase_product_in_storage Purchase_product_in_storage_Purchase_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Purchase_product_in_storage"
    ADD CONSTRAINT "Purchase_product_in_storage_Purchase_id_fkey" FOREIGN KEY ("Purchase_id") REFERENCES public."Purchases"("Purchase_id");
 v   ALTER TABLE ONLY public."Purchase_product_in_storage" DROP CONSTRAINT "Purchase_product_in_storage_Purchase_id_fkey";
       public          postgres    false    233    4792    236            �           2606    16532 "   Purchases Purchases_Worker_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Purchases"
    ADD CONSTRAINT "Purchases_Worker_id_fkey" FOREIGN KEY ("Worker_id") REFERENCES public."Workers"("Worker_id");
 P   ALTER TABLE ONLY public."Purchases" DROP CONSTRAINT "Purchases_Worker_id_fkey";
       public          postgres    false    4780    222    233            �           2606    24657 N   Refund_product_in_storage Refund_product_in_storage_Product_in_storage_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Refund_product_in_storage"
    ADD CONSTRAINT "Refund_product_in_storage_Product_in_storage_id_fkey" FOREIGN KEY ("Product_in_storage_id") REFERENCES public."Products_in_storage"("Product_in_storage_id");
 |   ALTER TABLE ONLY public."Refund_product_in_storage" DROP CONSTRAINT "Refund_product_in_storage_Product_in_storage_id_fkey";
       public          postgres    false    4782    239    224            �           2606    24652 B   Refund_product_in_storage Refund_product_in_storage_Refund_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Refund_product_in_storage"
    ADD CONSTRAINT "Refund_product_in_storage_Refund_id_fkey" FOREIGN KEY ("Refund_id") REFERENCES public."Refunds"("Refund_id");
 p   ALTER TABLE ONLY public."Refund_product_in_storage" DROP CONSTRAINT "Refund_product_in_storage_Refund_id_fkey";
       public          postgres    false    238    239    4798            �           2606    24642     Refunds Refunds_Purchase_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Refunds"
    ADD CONSTRAINT "Refunds_Purchase_id_fkey" FOREIGN KEY ("Purchase_id") REFERENCES public."Purchases"("Purchase_id");
 N   ALTER TABLE ONLY public."Refunds" DROP CONSTRAINT "Refunds_Purchase_id_fkey";
       public          postgres    false    4792    238    233            �           2606    24637    Refunds Refunds_Worker_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Refunds"
    ADD CONSTRAINT "Refunds_Worker_id_fkey" FOREIGN KEY ("Worker_id") REFERENCES public."Workers"("Worker_id");
 L   ALTER TABLE ONLY public."Refunds" DROP CONSTRAINT "Refunds_Worker_id_fkey";
       public          postgres    false    222    4780    238            �           2606    24776 (   Region_plans Region_plans_Region_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Region_plans"
    ADD CONSTRAINT "Region_plans_Region_id_fkey" FOREIGN KEY ("Region_id") REFERENCES public."Regions"("Region_id");
 V   ALTER TABLE ONLY public."Region_plans" DROP CONSTRAINT "Region_plans_Region_id_fkey";
       public          postgres    false    252    4814    254            �           2606    24788 "   Shop_plans Shop_plans_Shop_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Shop_plans"
    ADD CONSTRAINT "Shop_plans_Shop_id_fkey" FOREIGN KEY ("Shop_id") REFERENCES public."Shops"("Shop_id");
 P   ALTER TABLE ONLY public."Shop_plans" DROP CONSTRAINT "Shop_plans_Shop_id_fkey";
       public          postgres    false    4774    216    256            �           2606    24805 .   Shop_positions Shop_positions_Position_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Shop_positions"
    ADD CONSTRAINT "Shop_positions_Position_id_fkey" FOREIGN KEY ("Position_id") REFERENCES public."Positions"("Position_id");
 \   ALTER TABLE ONLY public."Shop_positions" DROP CONSTRAINT "Shop_positions_Position_id_fkey";
       public          postgres    false    229    258    4788            �           2606    24800 *   Shop_positions Shop_positions_Shop_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Shop_positions"
    ADD CONSTRAINT "Shop_positions_Shop_id_fkey" FOREIGN KEY ("Shop_id") REFERENCES public."Shops"("Shop_id");
 X   ALTER TABLE ONLY public."Shop_positions" DROP CONSTRAINT "Shop_positions_Shop_id_fkey";
       public          postgres    false    258    4774    216            �           2606    24810 ,   Shop_positions Shop_positions_Worker_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Shop_positions"
    ADD CONSTRAINT "Shop_positions_Worker_id_fkey" FOREIGN KEY ("Worker_id") REFERENCES public."Workers"("Worker_id");
 Z   ALTER TABLE ONLY public."Shop_positions" DROP CONSTRAINT "Shop_positions_Worker_id_fkey";
       public          postgres    false    258    4780    222            �           2606    24764    Shops Shops_Region_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Shops"
    ADD CONSTRAINT "Shops_Region_id_fkey" FOREIGN KEY ("Region_id") REFERENCES public."Regions"("Region_id") NOT VALID;
 H   ALTER TABLE ONLY public."Shops" DROP CONSTRAINT "Shops_Region_id_fkey";
       public          postgres    false    252    4814    216            �           2606    24750    Shops Shops_Shop_type_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Shops"
    ADD CONSTRAINT "Shops_Shop_type_id_fkey" FOREIGN KEY ("Shop_type_id") REFERENCES public."Shop_types"("Shop_type_id") NOT VALID;
 K   ALTER TABLE ONLY public."Shops" DROP CONSTRAINT "Shops_Shop_type_id_fkey";
       public          postgres    false    250    216    4812            �           2606    24609 #   Workers Workers_Worker_type_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Workers"
    ADD CONSTRAINT "Workers_Worker_type_id_fkey" FOREIGN KEY ("Worker_type_id") REFERENCES public."Worker_types"("Worker_type_id") NOT VALID;
 Q   ALTER TABLE ONLY public."Workers" DROP CONSTRAINT "Workers_Worker_type_id_fkey";
       public          postgres    false    235    4794    222            �   (   x�3�0��.콰��V����='r��qqq 6�o      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x�3�(�/�LI-�2��/�2b���� c��      �      x������ � �     