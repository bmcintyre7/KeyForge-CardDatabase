package com.keyforge.libraryaccess.LibraryAccessService.repositories

import com.keyforge.libraryaccess.LibraryAccessService.data.Expansion
import com.keyforge.libraryaccess.LibraryAccessService.data.Type
import org.springframework.data.jpa.repository.JpaRepository
import org.springframework.stereotype.Repository

@Repository
interface ExpansionRepository : JpaRepository<Expansion, Int> {
    fun findByName(name: String) : Expansion
}