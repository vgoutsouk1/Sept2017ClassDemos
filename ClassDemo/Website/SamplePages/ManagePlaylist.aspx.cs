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
        if(PlayList.Rows.Count == 0)
        {
            MessageUserControl.ShowInfo("warning", "No playlist has been retrieved");
        }
        else
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("warning", "No playlist name has been supplied");
            }
            else
            {
                //check only one row selected
                int trackid = 0;
                int tracknumber = 0; // optional
                int rowselected = 0; //search flag
                //create a pointer to use for the access of the 
                //gridview control
                CheckBox playlistselection = null;
                //traverse the gridview checking each row for a checked CheckBox
                for(int i = 0; i < PlayList.Rows.Count; i++)
                {
                    // find the checkbox on the indexed gridview row
                    //playlist selection will point to the checkbox
                    playlistselection = PlayList.Rows[i].FindControl("Selected") as CheckBox;
                    // if is checked
                    if (playlistselection.Checked)
                    {
                        trackid = int.Parse((PlayList.Rows[i].FindControl("TrackId") as Label).Text);
                        tracknumber = int.Parse((PlayList.Rows[i].FindControl("TrackNumber") as Label).Text);
                        rowselected++;
                    }
                } //eoFor loop
                if (rowselected != 1)
                {
                    MessageUserControl.ShowInfo("warning", "Select one track to move.");
                }
                else
                {
                    if(tracknumber == 1)
                    {
                        MessageUserControl.ShowInfo("Information", "Track can not be moved, already at the top of list.");
                    }
                    else
                    {
                        // at this point you have playlistname, username, trackid
                        //which is needed to move the track
                        //move the track Via BLL
                        MoveTrack(trackid, tracknumber, "up");

                    }
                }
            }
        }
    }

    protected void MoveDown_Click(object sender, EventArgs e)
    {
        //code to go here

        if (PlayList.Rows.Count == 0)
        {
            MessageUserControl.ShowInfo("warning", "No playlist has been retrieved");
        }
        else
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("warning", "No playlist name has been supplied");
            }
            else
            {
                //check only one row selected
                int trackid = 0;
                int tracknumber = 0; // optional
                int rowselected = 0; //search flag
                //create a pointer to use for the access of the 
                //gridview control
                CheckBox playlistselection = null;
                //traverse the gridview checking each row for a checked CheckBox
                for (int i = 0; i < PlayList.Rows.Count; i++)
                {
                    // find the checkbox on the indexed gridview row
                    //playlist selection will point to the checkbox
                    playlistselection = PlayList.Rows[i].FindControl("Selected") as CheckBox;
                    // if is checked
                    if (playlistselection.Checked)
                    {
                        trackid = int.Parse((PlayList.Rows[i].FindControl("TrackId") as Label).Text);
                        tracknumber = int.Parse((PlayList.Rows[i].FindControl("TrackNumber") as Label).Text);
                        rowselected++;
                    }
                } //eoFor loop
                if (rowselected != 1)
                {
                    MessageUserControl.ShowInfo("warning", "Select one track to move.");
                }
                else
                {
                    if (tracknumber == PlayList.Rows.Count)
                    {
                        MessageUserControl.ShowInfo("Information", "Track can not be moved, already at the bottom of list.");
                    }
                    else
                    {
                        // at this point you have playlistname, username, trackid
                        //which is needed to move the track
                        //move the track Via BLL
                        MoveTrack(trackid, tracknumber, "down");
                    }
                }
            }
        }
    }
    protected void MoveTrack(int trackid, int tracknumber, string direction)
    {
        //code to go here
        MessageUserControl.TryRun(() => {
            //standard call to bll
            PlaylistTracksController sysmger = new PlaylistTracksController();
            sysmger.MoveTrack(User.Identity.Name, PlaylistName.Text, trackid, tracknumber, direction);
            //refresh the list
            List<UserPlaylistTrack> results = sysmger.List_TracksForPlaylist(PlaylistName.Text, User.Identity.Name);
            PlayList.DataSource = results;
            PlayList.DataBind();
        },"Success","Track moved");
    }
    protected void DeleteTrack_Click(object sender, EventArgs e)
    {
        //code to go here
    }
}
