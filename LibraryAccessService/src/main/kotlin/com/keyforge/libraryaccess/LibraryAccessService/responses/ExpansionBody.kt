package com.keyforge.libraryaccess.LibraryAccessService.responses

import com.keyforge.libraryaccess.LibraryAccessService.data.Card

data class ExpansionBody (
        val name: String = "",
        val abbreviation: String = "",
        val number: String = "",
        val imageName: String = ""
)