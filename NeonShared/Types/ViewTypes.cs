using System;
using System.Collections.Generic;
using System.Text;

namespace NeonShared.Types
{
    public enum UwpViewTypes
    {
        ArtistLetters,
        AlbumLetters,
        AlbumArtistLetters,
        ArtistDetail,
        AlbumDetail,
        GenreLetters,
        LabelLetters,
        LabelsByLetter,
        RatingLetters,
        YearLetters,
        AddedDateYearLetters,
        AddedDateMonthLetters,
        AddedDateDayLetters,
        PlayedDateYearLetters,
        PlayedDateMonthLetters,
        PlayedDateDayLetters,
        FavouriteArtists,
        FavouriteAlbums,
        FavouriteTracks,
        Playlists,
        SmartPlaylists,
        SearchResults
    }

    public enum ViewTypeTracks
    {
        None,
        Genre,
        Rating,
        PlayedDate,
        FavouriteTracks,
        Playlists,
        SmartPlaylists
    }
}
