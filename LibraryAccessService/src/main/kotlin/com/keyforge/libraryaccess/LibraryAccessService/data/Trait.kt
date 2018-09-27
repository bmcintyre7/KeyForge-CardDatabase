package com.keyforge.libraryaccess.LibraryAccessService.data

import javax.persistence.Entity
import javax.persistence.Id
import javax.persistence.Table

@Entity
@Table(name = "trait")
data class Trait (
    @Id
    val id: Int? = null,
    val name: String = ""
)