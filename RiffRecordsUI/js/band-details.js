async function loadBandDetails() {
  const params = new URLSearchParams(window.location.search);
  const bandId = params.get("id");

  const wrapper = document.getElementById("bandDetailsWrapper");
  if (!bandId) {
    const emptyState = document.createElement("div");
    emptyState.className = "empty-state";

    const h2 = document.createElement("h2");
    h2.textContent = "No band selected";

    const p = document.createElement("p");
    const link = document.createElement("a");
    link.href = "shop.html";
    link.style.color = "var(--color-accent)";
    link.textContent = "Shop";
    p.textContent = "Go back to the ";
    p.appendChild(link);
    p.appendChild(document.createTextNode(" and pick one."));

    emptyState.appendChild(h2);
    emptyState.appendChild(p);
    wrapper.appendChild(emptyState);
    return;
  }

  try {
    const res = await fetch(`${API_BASE_URL}/api/band/${encodeURIComponent(bandId)}`);
    if (!res.ok && res.status !== 404) {
      throw new Error(`API error: ${res.status}`);
    }
    const band = res.status === 404 ? null : await res.json();

    if (!band) {
      const emptyState = document.createElement("div");
      emptyState.className = "empty-state";

      const h2 = document.createElement("h2");
      h2.textContent = "Band not found";

      const p = document.createElement("p");
      p.textContent = `"${bandId}" doesn't match anything in the catalog.`;

      emptyState.appendChild(h2);
      emptyState.appendChild(p);
      wrapper.appendChild(emptyState);
      return;
    }

    document.title = `${band.bandName} — RiffRecords`;

    document.getElementById("bandGenre").textContent = band.genre;
    document.getElementById("bandName").textContent = band.bandName;
    document.getElementById("bandBio").textContent = band.bio ?? "";
    document.getElementById("bandImage").src = band.bandPicture;
    document.getElementById("bandImage").alt = band.bandName;

    const grid = document.getElementById("vinylGrid");

    band.vinyls.forEach((v) => {
      const card = document.createElement("div");
      card.className = "vinyl-card";

      const media = document.createElement("div");
      media.className = "card-media";

      const img = document.createElement("img");
      img.src = v.image;
      img.alt = v.albumTitle;
      media.appendChild(img);

      const body = document.createElement("div");
      body.className = "card-body";

      const eyebrow = document.createElement("span");
      eyebrow.className = "eyebrow";
      eyebrow.textContent = v.releaseYear;

      const h3 = document.createElement("h3");
      h3.textContent = v.albumTitle;

      const priceSpan = document.createElement("span");
      priceSpan.className = "price";
      priceSpan.textContent = `$${v.price.toFixed(2)}`;

      const stock = document.createElement("span");
      stock.className = "stock-out";
      if (v.stock === 0) {
        stock.textContent = "Out of stock";
      } else {
        stock.style.color = "var(--color-muted)";
        stock.textContent = `${v.stock} in stock`;
      }

      body.appendChild(eyebrow);
      body.appendChild(h3);
      body.appendChild(priceSpan);
      body.appendChild(stock);

      const actions = document.createElement("div");
      actions.className = "card-actions";

      const btn = document.createElement("button");
      btn.className = "btn btn-primary btn-block add-to-cart";
      btn.dataset.id = v.vinylId;
      btn.dataset.title = v.albumTitle;
      btn.dataset.price = v.price;
      btn.dataset.image = v.image;
      if (v.stock === 0) {
        btn.disabled = true;
        btn.textContent = "Out of Stock";
      } else {
        btn.textContent = "Add to Cart";
      }

      actions.appendChild(btn);

      card.appendChild(media);
      card.appendChild(body);
      card.appendChild(actions);

      // Clicking the card (anywhere except Add to Cart) opens the album section below the grid.
      card.tabIndex = 0;
      card.setAttribute("role", "button");
      card.setAttribute("aria-label", `Show details for ${v.albumTitle}`);
      card.addEventListener("click", (e) => {
        if (e.target.closest(".add-to-cart")) return;
        showAlbumDetails(band, v, card);
      });
      card.addEventListener("keydown", (e) => {
        if (e.target !== card || (e.key !== "Enter" && e.key !== " ")) return;
        e.preventDefault();
        showAlbumDetails(band, v, card);
      });

      grid.appendChild(card);
    });
  } catch (err) {
    const emptyState = document.createElement("div");
    emptyState.className = "empty-state";

    const h2 = document.createElement("h2");
    h2.textContent = "Something went wrong";

    const p = document.createElement("p");
    p.textContent = "Couldn't load the catalog. Is the API running?";

    emptyState.appendChild(h2);
    emptyState.appendChild(p);
    wrapper.appendChild(emptyState);
    console.error(err);
  }
}

// Seconds -> "m:ss". Albums never pass an hour, so minutes can go above 59 ("74:12").
function formatDuration(seconds) {
  if (seconds == null) return "—";
  const m = Math.floor(seconds / 60);
  const s = seconds % 60;
  return `${m}:${String(s).padStart(2, "0")}`;
}

function el(tag, className, text) {
  const node = document.createElement(tag);
  if (className) node.className = className;
  if (text != null) node.textContent = text;
  return node;
}

function showAlbumDetails(band, v, card) {
  document.querySelectorAll(".vinyl-card.is-selected").forEach((c) => c.classList.remove("is-selected"));
  card.classList.add("is-selected");

  const section = document.getElementById("albumDetails");
  section.replaceChildren();
  const tracks = v.tracks ?? [];

  const layout = el("div", "container album-layout");

  const cover = el("img", "album-cover");
  cover.src = v.image;
  cover.alt = v.albumTitle;

  const info = el("div", "album-info");

  const head = el("div", "album-head");
  head.appendChild(el("span", "eyebrow", `${band.bandName} · ${v.releaseYear}`));
  head.appendChild(el("h2", null, v.albumTitle));

  const meta = el("dl", "album-meta");
  const addMeta = (label, value) => {
    const row = el("div");
    row.appendChild(el("dt", null, label));
    row.appendChild(el("dd", null, value));
    meta.appendChild(row);
  };
  addMeta("Label", v.label ?? "—");
  addMeta("Released", v.releaseYear);
  addMeta("Runtime", formatDuration(v.runtimeSeconds));
  addMeta("Tracks", tracks.length);

  info.appendChild(head);
  info.appendChild(meta);

  if (v.description) {
    info.appendChild(el("p", "album-description", v.description));
  }

  if (tracks.length > 0) {
    info.appendChild(el("h3", "tracklist-title", "Tracklist"));
    const list = el("ol", "tracklist");
    tracks.forEach((t) => {
      const li = el("li", t.isFamous ? "is-famous" : null);
      li.appendChild(el("span", "track-no", String(t.trackNumber).padStart(2, "0")));
      const name = el("span", "track-name", t.name);
      if (t.isFamous) name.appendChild(el("span", "famous-badge", "Most famous"));
      li.appendChild(name);
      li.appendChild(el("span", "track-length", formatDuration(t.durationSeconds)));
      list.appendChild(li);
    });
    info.appendChild(list);
  }

  const buy = el("div", "album-buy");
  buy.appendChild(el("span", "price", `$${v.price.toFixed(2)}`));
  buy.appendChild(el("span", v.stock === 0 ? "stock-out" : "stock-in", v.stock === 0 ? "Out of stock" : `${v.stock} in stock`));

  const btn = el("button", "btn btn-primary add-to-cart", v.stock === 0 ? "Out of Stock" : "Add to Cart");
  btn.dataset.id = v.vinylId;
  btn.dataset.title = v.albumTitle;
  btn.dataset.price = v.price;
  btn.dataset.image = v.image;
  btn.disabled = v.stock === 0;
  buy.appendChild(btn);
  info.appendChild(buy);

  layout.appendChild(cover);
  layout.appendChild(info);
  section.appendChild(layout);

  section.hidden = false;
  section.scrollIntoView({ behavior: "smooth", block: "start" });
}

document.addEventListener("DOMContentLoaded", loadBandDetails);