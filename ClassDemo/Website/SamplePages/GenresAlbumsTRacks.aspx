<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GenresAlbumsTRacks.aspx.cs" Inherits="SamplePages_GenresAlbumsTRacks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <h1> Genres Albums Tracks</h1>

    <!--    inside a repeater you need a minimum of a Itemplate
            other templates include HeaderTemplate, FooterTemplate,
            AlternatingItemTemplate, SeparatorTemplate
        
        outer repeater will display the first fields from the DTO class 
            which do not repeat
        outer repeater gets its data from an ODS
        
        nested repeater will display the collection of the DTO file
        nested repeater will get its datasource from the collection of the
            DTO class(either a POCO or another DTO)
        
        This pattern repeats for all levels of your dataset 
        -->

    <asp:Repeater ID="GenreAlbumTrackList" runat="server"
         DataSourceID="GenreAlbumTrackListODS"
        ItemType ="Chinook.Data.DTOs.GenreDTO">
        <ItemTemplate>
            <h2>Genre: <%# Eval("genre")%></h2>
            <asp:Repeater ID="GenreAlbums" runat="server" 
                DataSource =' <%# Eval("albums") %>'
                ItemType ="Chinook.Data.DTOs.AlbumDTO">
                <ItemTemplate>
                    <strong>Album:
                        <%# string.Format("{0}  ({1}) Tracks: {2}",Eval("Title"), Eval("releaseyear"), Eval("numberoftracks")) %>

                    </strong>

                    <asp:ListView ID="AlbumTracks" runat="server"
                        ItemType=" Chinook.Data.POCOs.TrackPOCO"
                        DataSource="<%# Item.tracks %>">
                        <LayoutTemplate>
                            <table>
                                <tr>
                                    <th>Song</th>
                                    <th>Length</th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server"></tr>
                            </table>
                        </LayoutTemplate>

                        <ItemTemplate> <%--placeholder are put in here--%>
                           <tr style="background-color:lightgrey">
                               <td style="width:600px"><%# Item.song %></td>
                               <td><%# Item.length %></td>
                           </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                               <td style="background-color:papayawhip"><%# Item.song %></td>
                               <td style="background-color:papayawhip"><%# Item.length %></td>
                           </tr>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                            <tr>
                               <td colspan="2">No data available at this time</td>
                               
                           </tr>
                        </EmptyDataTemplate>


                    </asp:ListView>

         <%--           <asp:GridView ID="AlbumTracks" runat="server"
                        ItemType=" Chinook.Data.POCOs.TrackPOCO"
                        DataSource='<%# Item.tracks %>' AutoGenerateColumns="False">


                        <Columns>
                            <asp:TemplateField HeaderText="Song">
                                <AlternatingItemTemplate >
                                    <asp:Label runat="server" Text="<%# Item.song %>"
                                        BackColor="#c0c0c0">


                                    </asp:Label>
                                    
                                </AlternatingItemTemplate>
                                
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Length">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text="<%# Item.length %>"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>--%>
                    </br>
                    <%--<asp:Repeater ID="AlbumTracks" runat="server"
                       DataSource =" <%# Item.tracks %>" 
                        ItemType =" Chinook.Data.POCOs.TrackPOCO">
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <th>Song</th>
                                    <th>Length</th>
                                </tr>

                            
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="width:600px">
                                    <%# Item.song %>

                                </td>
                                <td>
                                    <%#Item.length %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                        
                    </asp:Repeater>--%>
                </ItemTemplate>
                <SeparatorTemplate>
                    <hr style="height:3px;border:none;color:#000;background-color:#000;"/>
                </SeparatorTemplate>
            </asp:Repeater>
        </ItemTemplate>
    </asp:Repeater>


    <asp:ObjectDataSource ID="GenreAlbumTrackListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Genre_GenreAlbumTracks" 
        TypeName="ChinookSystem.BLL.GenreController">

    </asp:ObjectDataSource>
</asp:Content>

