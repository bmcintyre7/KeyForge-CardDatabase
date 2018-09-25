package com.keyforge.libraryaccess.LibraryAccessService.repositories

import com.keyforge.libraryaccess.LibraryAccessService.data.Rarity
import org.springframework.data.jpa.repository.JpaRepository
import org.springframework.stereotype.Repository

@Repository
interface RarityRepository : JpaRepository<Rarity, Int> {
    fun findByName(name: String) : Rarity
}