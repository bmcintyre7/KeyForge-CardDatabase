package com.keyforge.libraryaccess.LibraryAccessService.data

import javax.persistence.Entity
import javax.persistence.Id

@Entity
data class Type (
    @Id
    val id: Int = 0,
    val name: String = ""
)