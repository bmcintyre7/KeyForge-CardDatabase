package com.keyforge.libraryaccess.LibraryAccessService.responses

data class CardBody (
    val name:String = "",
    val type: String = "",
    val text: String = "",
    val aember: String? = null,
    val armor: String? = null,
    val power: String? = null,
    val rarity: String = "",
    val artist: String = "",
    val imageNames: MutableList<String>,
    val expansions: MutableList<String>,
    val houses: MutableList<String>,
    val keywords: MutableList<String>,
    val traits: MutableList<String>
)