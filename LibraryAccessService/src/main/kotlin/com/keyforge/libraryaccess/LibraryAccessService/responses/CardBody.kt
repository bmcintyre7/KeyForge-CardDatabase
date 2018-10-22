package com.keyforge.libraryaccess.LibraryAccessService.responses

import com.keyforge.libraryaccess.LibraryAccessService.data.Card
import kotlin.math.exp

data class CardBody (
    val name: String = "",
    val imageNames: MutableList<String>,
    val expansions: List<ExpansionBody>
)