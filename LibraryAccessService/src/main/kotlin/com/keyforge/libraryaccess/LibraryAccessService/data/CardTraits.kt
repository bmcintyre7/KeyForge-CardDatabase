package com.keyforge.libraryaccess.LibraryAccessService.data

import com.fasterxml.jackson.annotation.JsonIgnore
import org.hibernate.annotations.BatchSize
import javax.persistence.*

@Entity
@Table(name = "cardTraits")
data class CardTraits (
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    val id: Int? = null,
    @JsonIgnore
    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "cardId")
    @BatchSize(size=50)
    val card: Card,
    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "traitId")
    @BatchSize(size=50)
    val trait: Trait
)