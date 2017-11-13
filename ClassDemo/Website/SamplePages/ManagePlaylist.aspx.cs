﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using ChinookSystem.BLL;
using Chinook.Data.POCOs;

#endregion
public partial class SamplePages_ManagePlaylist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.IsAuthenticated)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else
        {
            TracksSelectionList.DataSource = null;
        }
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {
        //PreRenderComplete occurs just after databinding page events
        //load a pointer to point to your DataPager control
        DataPager thePager = TracksSelectionList.FindControl("DataPager1") as DataPager;
        if (thePager !=null)
        {
            //this code will check the StartRowIndex to see if it is greater that the
            //total count of the collection
            if (thePager.StartRowIndex > thePager.TotalRowCount)
            {
                thePager.SetPageProperties(0, thePager.MaximumRows, true);
            }
        }
    }

    protected void ArtistFetch_Click(object sender, EventArgs e)
    {
        //code to go here
        TracksBy.Text = "Artist"; ;
        SearchArgID.Text = ArtistDDL.SelectedValue;
        TracksSelectionList.DataBind();

    }

    protected void MediaTypeFetch_Click(object sender, EventArgs e)
    {
        //code to go here
        TracksBy.Text = "MediaType"; ;
        SearchArgID.Text = MediaTypeDDL.SelectedValue;
        TracksSelectionList.DataBind();
    }

    protected void GenreFetch_Click(object sender, EventArgs e)
    {
        //code to go here
        TracksBy.Text = "Genre"; ;
        SearchArgID.Text = GenreDDL.SelectedValue;
        TracksSelectionList.DataBind();
    }

    protected void AlbumFetch_Click(object sender, EventArgs e)
    {
        //code to go here
        TracksBy.Text = "Album"; ;
        SearchArgID.Text = AlbumDDL.SelectedValue;
        TracksSelectionList.DataBind();
    }

    protected void PlayListFetch_Click(object sender, EventArgs e)
    {
        //code to go here
        //standard query
        if (string.IsNullOrEmpty(PlaylistName.Text))
        {
            //put out an error message
            //this form uses a user control called MessageUserControl
            //the user control will be the mechanism to display messages on this form
            MessageUserControl.ShowInfo("Warning", "Playlist name is required");
        }
        else
        {
            //MessageUserControl already has the try/catch cosing embedded in the control
            MessageUserControl.TryRun(() =>

            {
                //this is the process coding block to be executed under the
                //"watchful eye" of the MessageUser control

                //obtain the user name from the security part of the application
                string username = User.Identity.Name;
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                List<UserPlaylistTrack> playlist = sysmgr.List_TracksForPlaylist
                (PlaylistName.Text, username);
                PlayList.DataSource = playlist;
                PlayList.DataBind();
            },"//title","here is yout current plalist.");
        }
    }

    protected void TracksSelectionList_ItemCommand(object sender, 
        ListViewCommandEventArgs e)
    {
        //code to go here
        //ListViewCommandEventArgs parameter e contains the CommandArg value
        if (string.IsNullOrEmpty(PlaylistName.Text))
        {
         
            MessageUserControl.ShowInfo("Warning", "Playlist name is required");
        }
        else
        {
            string username = User.Identity.Name;
            // the TrackID is going to come from e.CommandArgument
            // e.CommandArgument is an object therefore convert to string 
            int trackid = int.Parse(e.CommandArgument.ToString());

            //the follwing code calls a BLL method to add to the database
            MessageUserControl.TryRun(() =>
            {

                PlaylistTracksController sysmger = new PlaylistTracksController();
                List<UserPlaylistTrack> refreshresults = sysmger.Add_TrackToPLaylist(PlaylistName.Text, username, trackid);
                PlayList.DataSource = refreshresults;
                PlayList.DataBind();
            }, "Success", "Track added to the playlist");
            
        }
    }

    protected void MoveUp_Click(object sender, EventArgs e)
    {
        //code to go here
    }

    protected void MoveDown_Click(object sender, EventArgs e)
    {
        //code to go here
    }
    protected void MoveTrack(int trackid, int tracknumber, string direction)
    {
        //code to go here
    }
    protected void DeleteTrack_Click(object sender, EventArgs e)
    {
        //code to go here
    }
}
