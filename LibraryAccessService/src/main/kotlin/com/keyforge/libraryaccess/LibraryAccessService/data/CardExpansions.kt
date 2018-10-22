package com.keyforge.libraryaccess.LibraryAccessService.data

import com.fasterxml.jackson.annotation.JsonIgnore
import org.hibernate.annotations.BatchSize
import javax.persistence.*

@Entity
@Cacheable(value = true)
@Table(name = "cardExpansions")
data class CardExpansions (
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    val id: Int? = null,
    @JsonIgnore
    @OneToOne(fetch = FetchType.LAZY)
    @BatchSize(size=50)
    @JoinColumn(name = "cardId")
    val card: Card,
    @OneToOne(fetch = FetchType.LAZY)
    @BatchSize(size=50)
    @JoinColumn(name = "expansionId")
    val expansion: Expansion,
    val number: String = ""
)