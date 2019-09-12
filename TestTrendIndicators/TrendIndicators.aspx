<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="TrendIndicators.aspx.cs" Inherits="TestTrendIndicators.TrendIndicators" %>

<%@ Register assembly="DevExpress.XtraCharts.v18.2.Web, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:DropDownList runat="server" ID="ddlDevices" OnSelectedIndexChanged="ddlDevices_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

    <dx:WebChartControl ID="WebChartControl1" runat="server" Height="420px"
        Width="700px"  ClientInstanceName="chart" EnableViewState="False" SaveStateOnCallbacks="False" CrosshairEnabled="True">
        <borderoptions visibility="False"/>
        <DiagramSerializable>
            <dx:XYDiagram>
                <AxisX Title-Text="Date" VisibleInPanesSerializable="1" Interlaced="True" visibility="True">
                    <DateTimeScaleOptions MeasureUnit="Day" GridAlignment="Month" WorkdaysOnly="True" AutoGrid="False" GridSpacing="0.5">
                    </DateTimeScaleOptions>
                    <WholeRange Auto="False" MinValueSerializable="7/1/2014 00:00:00.000" MaxValueSerializable="12/31/2014 00:00:00.000" AutoSideMargins="False" SideMarginsValue="0.7"></WholeRange>
                    <GridLines Visible="True" minorvisible="True"></GridLines>
                </AxisX>
                <AxisY Title-Text="US Dollars" VisibleInPanesSerializable="-1" alignment="Far">
                    <visualrange auto="False" maxvalueserializable="120" minvalueserializable="95" /><WholeRange AlwaysShowZeroLevel="False"  ></WholeRange>
                    <GridLines MinorVisible="True"></GridLines>
                </AxisY>
                <secondaryaxesy>
                    <dx:SecondaryAxisY AxisID="0" Name="MassIndexAxisY" Title-Text="Mass Index" VisibleInPanesSerializable="0">
                        <wholerange alwaysshowzerolevel="False" />
                        <gridlines minorvisible="True" visible="True"/>
                    </dx:SecondaryAxisY>
                    <dx:SecondaryAxisY AxisID="1" Name="StDevAxisY" VisibleInPanesSerializable="1">
                        <wholerange alwaysshowzerolevel="False" />
                        <gridlines minorvisible="True" visible="True"/>
                    </dx:SecondaryAxisY>
                </secondaryaxesy>
                <defaultpane LayoutOptions-RowSpan="2">
                </defaultpane>
                <panes>
                    <dx:XYDiagramPane Name="MassIndexPane" PaneID="0">
                    </dx:XYDiagramPane>
                    <dx:XYDiagramPane Name="StDevPane" PaneID="1">
                    </dx:XYDiagramPane>
                </panes>
            </dx:XYDiagram>
        </DiagramSerializable>
        <legend alignmenthorizontal="Left" visibility="True"></legend>
        <Legends>
            <dx:Legend AlignmentHorizontal="Left" DockTargetName="MassIndexPane" Name="MassIndexLegend">
            </dx:Legend>
            <dx:Legend AlignmentHorizontal="Left" DockTargetName="StDevPane" Name="StDevLegend">
            </dx:Legend>
        </Legends>
        <SeriesSerializable>
            <dx:Series Name="USDJPY Daily" ArgumentScaleType="DateTime" LabelsVisibility="False"
                ArgumentDataMember="DateTimeArgument" ValueDataMembersSerializable="LowValue;HighValue;OpenValue;CloseValue">                
                <ViewSerializable>
                    <dx:StockSeriesView LevelLineLength="0.3">
                        <Indicators>
                            <dx:BollingerBands Name="Bollinger Bands" ShowInLegend="True"></dx:BollingerBands>
                            <dx:MassIndex AxisYName="MassIndexAxisY" LegendName="MassIndexLegend" Name="Mass Index" PaneName="MassIndexPane" ShowInLegend="True">
                                <linestyle thickness="2" />
                            </dx:MassIndex>
                            <dx:StandardDeviation AxisYName="StDevAxisY" LegendName="StDevLegend" Name="Standard Deviation" PaneName="StDevPane" ShowInLegend="True">
                                <linestyle thickness="2" />
                            </dx:StandardDeviation>
                        </Indicators>
                    </dx:StockSeriesView>
                </ViewSerializable>
                <LabelSerializable>
                    <dx:StockSeriesLabel ResolveOverlappingMode="Default"></dx:StockSeriesLabel>
                </LabelSerializable>
            </dx:Series>
        </SeriesSerializable>       
        <Titles>
            <dx:ChartTitle Text="Stock Prices" Visibility="True"></dx:ChartTitle>
        </Titles>
        <crosshairoptions showonlyinfocusedpane="False">
        </crosshairoptions>
    </dx:WebChartControl>  
</asp:Content>
