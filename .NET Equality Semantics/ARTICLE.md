# Equality Semantics in .NET
[![License: CC BY 4.0](https://img.shields.io/badge/License-CC%20BY%204.0-lightgrey.svg)](https://creativecommons.org/licenses/by/4.0/)


### Equality Semantics: At a Glance

- `.Equals` behaves differently for reference types and value types. For reference types, `.Equals` often defers to `.ReferenceEquals` and considers two objects to be **equal** only if they are, in fact, the **same instance** of an object. For value types, `.Equals` performs a **value-based** comparison.
- Reference equality can be problematic— **especially** for immutable types.  If you are implementing an immutable type, you should always implement `IEquatable<T>` and perform **value comparison** instead of **reference comparison**.
- When immutable types ***change***, **a new instance** of the type is created and returned, making reference equality useless.

There are of course other reasons you may wish to implement value equality for a reference type.

When implementing `IEquatable<T>`, it is rarely correct to only implement `bool Equals(T other)` like the interface demands.

Implement the following as well:

- ### `GetHashCode`
Since you are not relying on the base equality comparison, you must implement GetHashCode yourself.  It is not uncommon for systems (even within core .NET framework code itself) to call GetHashCode to determine equality.
- ### Override `object.Equals`
Non-strongly typed collections and apis do not use `IEquatable<T>`.  They don’t support or are not aware of generics, and so must use object.Equals.  This includes ArrayList, Hashtable and others.
- ### `operator ==` and `operator !=`
Do not assume that these are implemented in terms of .Equals.  To guarantee expected behavior, implement these as well.

**Implementing equality semantics yourself is difficult and error prone** (see: https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode/263416#263416 to start to get an idea).  I recommend that instead, you **leverage tuples to do the heavy lifting for you.**  

By packing fields into a tuple, like `(field1, field2, ...)` and then using the tuple to do comparison, we can offload equality logic and ensure consistency.  The sample code in TupleEqualityTest uses tuples to implement equality semantics.

### Modern Take
C# 9.0 introduced `record` types and improved pattern matching, making structural equality easier than ever.  For new codebases targeting .NET 5+, consider using `record` for immutable types where value semantics are important.
```csharp
public record Point(float x, float y, float z);
``` 
The compiler generates Equals, GetHashCode, ==, and != for you.

### Reference Equality Still Matters
Not all systems play well with value-based equality. For example, **Entity Framework Core** relies on **reference equality** to track changes and ensure that each entity in memory maps to a unique row in the database. If you use a `record` or `record struct` for an entity type, EF Core may misbehave — treating distinct instances with the same data as the **same** object.
Stick with classes and avoid value equality semantics when working with ORMs like EF Core.

### Key Observations from Unit Tests
These tests reinforce the differences between **value equality** and **reference equality**, and how they behave in both custom `IEquatable<T>` types and C# `record` types.

- **Value equality** works as expected for both `Point` (tuple-based) and `Trivial` (record-based) types — even when the instances are not the same reference.

```csharp
var a = new Point(1, 2, 3);
var b = new Point(1, 2, 3);

a.Equals(b);    // true
a == b;         // true
ReferenceEquals(a, b); // false
```

- **Reference equality still matters.** Objects with equal values are still distinct instances unless explicitly reused.

```csharp
var a = new Trivial(1, 2, 3);
var b = new Trivial(1, 2, 3);

a == b;                         // true (value equality)
object.ReferenceEquals(a, b);  // false (they are distinct instances)
```

- **Same instance** always satisfies both reference and value equality.

```csharp
var a = new Trivial(1, 2, 3);

a == a;                         // true
a.Equals(a);                    // true
object.ReferenceEquals(a, a);  // true
```

These examples highlight the importance of understanding how equality is implemented and when reference identity might still be relevant — even when working with immutable, value-oriented types.

### Deeper Dive: Topics to Explore on Your Own

This post focuses on the most common and practical aspects of equality in .NET, especially for immutable types. That said, equality semantics can go much deeper — and more nuanced — depending on what you're building.

If you're interested in taking a self-directed deeper dive, here are some advanced topics worth exploring:

- **Equality and Hash-Based Collections**  
  Learn how equality and `GetHashCode` impact behavior in `Dictionary<TKey, TValue>` and `HashSet<T>`, especially when using custom objects as keys.

- **Floating-Point Equality**  
  Understand the challenges of comparing `float` and `double` values due to rounding and precision loss, and how to use tolerances or `MathF.Abs(a - b) < epsilon`.

- **Inheritance and Equality Pitfalls**  
  Discover why implementing value equality in base classes can be risky and why sealed types or records are often the safer path.

- **Structural Equality vs Referential Equality**  
  Explore how .NET defines and handles structural comparisons, and where reference identity is still relevant.

These aren't required to understand the basics, but they’re great next steps if you want to master equality semantics at a deeper level.

### Useful Links
- [IEquatable<T> documentation](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1?view=netcore-3.0)
- [Jared Parsons: Why you must override Equals and GetHashCode](https://blogs.msdn.microsoft.com/jaredpar/2009/01/15/if-you-implement-iequatablet-you-still-must-override-objects-equals-and-gethashcode/)
- [StackOverflow: Best GetHashCode algorithm](https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode/263416#263416)
- [IntelliTect: Using Tuples to Implement Object Equality](https://intellitect.com/blog/overidingobjectusingtuple/)
- [C# 9 Record Types](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record)

---
**License:** [CC BY 4.0](https://creativecommons.org/licenses/by/4.0/) by _Gary McNickle_

> Example attribution:  
> “Based on work by Gary McNickle (CC BY 4.0) — [GitHub](https://github.com/gmcnickle)”

---

*Written by Gary McNickle — Engineering Manager, .NET enthusiast, and mentor.  
Helping developers grow with clarity and practical examples.*  
*[GitHub](https://github.com/gmcnickle) • [LinkedIn](https://www.linkedin.com/in/gmcnickle/)*
