
CREATE VIEW [dbo].[vwIF511DataAll]
AS
SELECT        SUBSTRING(a.TRANSDATA, 1, 7) AS groupagreementcustno, CAST(SUBSTRING(a.TRANSDATA, 1, 7) AS varchar(50)) AS Custno_lang, SUBSTRING(a.TRANSDATA, 8, 4) AS product_receiver_no, 
                         CAST(SUBSTRING(a.TRANSDATA, 12, 11) AS varchar(50)) AS Invoice_no, CAST(SUBSTRING(a.TRANSDATA, 26, 4) AS varchar(10)) AS Consecutive_card_no, CAST(SUBSTRING(a.TRANSDATA, 30, 8) 
                         AS varchar(20)) AS Purchase_date, SUBSTRING(a.TRANSDATA, 38, 2) + ':' + SUBSTRING(a.TRANSDATA, 40, 2) + ':' + SUBSTRING(a.TRANSDATA, 42, 2) AS Purchase_time, CAST(SUBSTRING(a.TRANSDATA, 44, 
                         7) AS varchar(50)) AS Receipt_no, CAST(SUBSTRING(a.TRANSDATA, 71, 7) AS varchar(50)) AS Phase_no, SUBSTRING(a.TRANSDATA, 78, 2) AS Purchase_type, CAST(SUBSTRING(a.TRANSDATA, 83, 3) 
                         AS varchar(10)) AS Product_group2, CAST(SUBSTRING(a.TRANSDATA, 86, 9) AS decimal(10, 2)) / 100 AS Quantity, CAST(SUBSTRING(a.TRANSDATA, 95, 10) AS decimal(11, 2)) / 100 AS Price, 
                         CAST(SUBSTRING(a.TRANSDATA, 105, 10) AS decimal(11, 2)) / 100 AS Gross, CAST(SUBSTRING(a.TRANSDATA, 115, 10) AS decimal(11, 2)) / 100 AS Rebate_campaign, CAST(SUBSTRING(a.TRANSDATA, 125, 10) 
                         AS decimal(11, 2)) / 100 AS Net, CAST(SUBSTRING(a.TRANSDATA, 135, 10) AS decimal(11, 2)) / 100 AS VAT, CAST(SUBSTRING(a.TRANSDATA, 145, 10) AS decimal(11, 2)) / 100 AS Energy_tax, 
                         CAST(SUBSTRING(a.TRANSDATA, 155, 10) AS decimal(11, 2)) / 100 AS Specification_fee, SUBSTRING(a.TRANSDATA, 165, 6) AS Reg_no, SUBSTRING(a.TRANSDATA, 171, 10) AS Info, SUBSTRING(a.TRANSDATA, 
                         181, 20) AS Foreign_card_no, SUBSTRING(a.TRANSDATA, 201, 8) AS Order_no, CAST(SUBSTRING(a.TRANSDATA, 209, 10) AS decimal(11, 2)) / 100 AS rabattavrakforbund, CAST(SUBSTRING(a.TRANSDATA, 219, 10) 
                         AS decimal(11, 2)) / 100 AS rabattavrakforening, CAST(SUBSTRING(a.TRANSDATA, 229, 10) AS decimal(11, 2)) / 100 AS rabattavrakmedlemforbund, SUBSTRING(a.TRANSDATA, 239, 1) AS forbrukstat, 
                         CAST(SUBSTRING(a.TRANSDATA, 251, 7) AS decimal(9, 3)) / 1000 AS prisredpriskrig, CAST(SUBSTRING(a.TRANSDATA, 258, 10) AS decimal(11, 2)) / 100 AS prisfakt, CAST(SUBSTRING(a.TRANSDATA, 268, 10) 
                         AS decimal(11, 2)) / 100 AS bruttobeloppfakt, CAST(SUBSTRING(a.TRANSDATA, 278, 10) AS decimal(11, 2)) / 100 AS rabattfakt, SUBSTRING(a.TRANSDATA, 288, 1) AS priskrigrabred, 0 AS Basic_rebate, 
                         CASE substring(b.transdata, 110, 5) WHEN ' ' THEN 0 ELSE substring(b.transdata, 110, 5) END AS Agreement_No, CAST(SUBSTRING(b.TRANSDATA, 116, 4) AS varchar(50)) AS Customer_category_no, 
                         CAST(SUBSTRING(b.TRANSDATA, 120, 3) AS varchar(50)) AS Association_no, CAST(SUBSTRING(c.TRANSDATA, 170, 8) AS varchar(20)) AS Payment_date, CAST(SUBSTRING(b.TRANSDATA, 195, 8) 
                         AS varchar(50)) AS Invoice_date, ' ' AS Rebate_unit, SUBSTRING(a.TRANSDATA, 289, 35) AS Card_subject, 0 AS Invoice_amount, 0 AS Invoice_rebate_amount, 0 AS Expr1, 0 AS Basic_rebate_amount, 
                         11 AS System_part, 1 AS Origin
FROM            (SELECT        ID, TYP, TRANSDATA
                          FROM            dbo.IF511_Split) AS a INNER JOIN
                             (SELECT        ID, TYP, TRANSDATA
                               FROM            dbo.IF511_Split AS IF511_Split_2) AS b ON a.ID = b.ID INNER JOIN
                             (SELECT        ID, TYP, TRANSDATA
                               FROM            dbo.IF511_Split AS IF511_Split_1) AS c ON b.ID = c.ID
WHERE        (LEFT(a.TYP,6) = 'BVF170') AND (LEFT(b.TYP,6) = 'BVF120') AND (LEFT(c.TYP,6) = 'BVF190')

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 119
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 119
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 119
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwIF511DataAll';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwIF511DataAll';

