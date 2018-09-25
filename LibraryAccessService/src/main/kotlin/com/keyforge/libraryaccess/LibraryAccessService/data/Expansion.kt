package com.keyforge.libraryaccess.LibraryAccessService.data

import javax.persistence.Entity
import javax.persistence.Id
import javax.persistence.Table

@Entity
@Table(name = "expansion")
data class Expansion (
    @Id
    val id: Int = 0,
    val name: String = ""
)