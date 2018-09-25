package com.keyforge.libraryaccess.LibraryAccessService.data

import javax.persistence.Entity
import javax.persistence.Id
import javax.persistence.Table

@Entity
@Table(name = "rarity")
data class Rarity (
    @Id
    val id: Int? = null,
    val name: String = ""
)